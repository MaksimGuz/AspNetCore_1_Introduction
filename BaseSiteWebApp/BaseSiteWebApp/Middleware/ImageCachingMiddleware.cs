using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BaseSiteWebApp.Models;
using AsyncUtilities;

namespace BaseSiteWebApp.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ImageCachingOptions _imageCachingOptions;
        private readonly ILogger<ImageCachingMiddleware> _logger;
        private ConcurrentDictionary<string, CachedImageInfo> cachedImages = new ConcurrentDictionary<string, CachedImageInfo>();
        private ConcurrentDictionary<string, DateTime> filesToDelete = new ConcurrentDictionary<string, DateTime>();
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private bool imageDirectoryExists = false;
        //private readonly string imageCachingLockKey = typeof(ImageCachingMiddleware).FullName;
        private readonly StripedAsyncLock<string> _lock = null;
        private readonly object _lockObject = new object();

        public ImageCachingMiddleware(RequestDelegate next, IOptions<ImageCachingOptions> optionsAccessor, ILogger<ImageCachingMiddleware> logger)
        {
            _next = next;
            _imageCachingOptions = optionsAccessor.Value;
            _lock = new StripedAsyncLock<string>(stripes: _imageCachingOptions.MaxStripesForLockAsync);
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Image caching middleware start {context.Request.Path.Value}");

            // replace the output stream to collect the result
            var responseStream = context.Response.Body;
            var buffer = new MemoryStream();
            var reader = new StreamReader(buffer);
            if (IsRequestToCategoriesImages(context))
            {
                context.Response.Body = buffer;
                if (await FillResponseStreamFromCache(context, responseStream, context.Request.Path.Value))
                    return;
            }

            await _next(context);
            _logger.LogInformation($"Image caching middleware after next. {context.Request.Path.Value}");

            if (IsRequestToCategoriesImages(context))
            {
                try
                {
                    // reset the buffer and retrieve the content
                    buffer.Seek(0, SeekOrigin.Begin);
                    var responseBody = await reader.ReadToEndAsync();

                    await AddImageToCache(context, buffer);
                    CleanupCachedFiles();

                    // copy back our buffer to the response stream
                    buffer.Seek(0, SeekOrigin.Begin);
                    await buffer.CopyToAsync(responseStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ImageCachingMiddleware error");
                }
                finally
                {
                    context.Response.Body = responseStream;
                }
            }
            _logger.LogInformation($"Image caching middleware finish. {context.Request.Path.Value}");
        }
        private bool IsRequestToCategoriesImages(HttpContext context)
        {
            return context != null && context.Response != null && context.Request.Path != null &&
                context.Request.Path.Value.StartsWith("/Categories/Image/");
        }

        private async Task<bool> FillResponseStreamFromCache(HttpContext context, Stream responseStream, string requestPath)
        {
            using (await _lock.LockAsync(context.Request.Path.Value)) //using (new ImageCacheItemLock(image.FilePath)) //using(await _lock.UseWaitAsync())
            {
                if (cachedImages.TryGetValue(requestPath, out CachedImageInfo image))
                {
                    if (image.Expires > DateTime.UtcNow)
                    {
                        _logger.LogInformation($"------START GETTING IMAGE FROM CACHE. {context.Request.Path.Value}");
                        context.Response.ContentType = image.ContentType;
                        if (!File.Exists(image.FilePath))
                            return false;
                        using (var fileStream = new FileStream(image.FilePath, FileMode.Open))
                        {
                            await fileStream.CopyToAsync(responseStream);
                        }
                        context.Response.Body = responseStream;
                        _logger.LogInformation($"------STOP  GETTING IMAGE FROM CACHE. {context.Request.Path.Value} from file {image.FilePath}");
                        return true;
                    }
                }
            }
            return false;
        }

        private async Task AddImageToCache(HttpContext context, Stream buffer)
        {
            lock(_lockObject)// using (await _semaphoreSlim.UseWaitAsync()) //using (await _lock.LockAsync(imageCachingLockKey))
            { // remove expired items from cache
                _logger.LogInformation($"------START CLEAN UP. {context.Request.Path.Value}");
                if (cachedImages.Any(f => f.Value.Expires < DateTime.UtcNow))
                {
                    foreach(var path in cachedImages.Where(f => f.Value.Expires < DateTime.UtcNow).Select(x => x.Value.FilePath).ToList())
                    {
                        if (!cachedImages.TryRemove(context.Request.Path.Value, out CachedImageInfo imageOut))
                            _logger.LogInformation($"***** file {path} was not removed from cachedImages for request {context.Request.Path.Value}");
                        else
                        {
                            _logger.LogInformation($"***** file {path} was succesfully removed from cachedImages for request {context.Request.Path.Value}");
                            if (!filesToDelete.TryAdd(path, imageOut.ExpiresForDeleteFile))
                                _logger.LogInformation($"***** file {path} was not added to filesToDelete for request {context.Request.Path.Value}");
                            else
                                _logger.LogInformation($"***** file {path} was succesfully added to filesToDelete for request {context.Request.Path.Value}");
                        }

                    }                    
                }
                _logger.LogInformation($"------STOP CLEAN UP. {context.Request.Path.Value}");
            }
            if (cachedImages.Count() == _imageCachingOptions.MaxImages)
                return; // cache is full

            if (!cachedImages.TryGetValue(context.Request.Path.Value, out CachedImageInfo image))
            {
                string filePath = $"{_imageCachingOptions.CacheDirectoryPath}{context.Request.Path.Value.Replace("/", "")}{DateTime.UtcNow.ToString("_yyyy_mm_dd_hh_mm", CultureInfo.InvariantCulture)}.bmp";
                using (await _lock.LockAsync(context.Request.Path.Value)) //using (new ImageCacheItemLock(filePath)) //using(await _lock.UseWaitAsync())
                {
                    _logger.LogInformation($"------START ADDING IMAGE TO CACHE. {context.Request.Path.Value}");
                    EnsureDestinationFolderExist(_imageCachingOptions.CacheDirectoryPath);
                    await WriteResponseBodyToDisk(buffer, filePath);
                    _logger.LogInformation($"Image {filePath} saved to disk. {context.Request.Path.Value}");
                    if (!cachedImages.TryAdd(context.Request.Path.Value, new CachedImageInfo(filePath, context.Response.ContentType, _imageCachingOptions)))
                        _logger.LogInformation($"***** file {filePath} were not added to cachedImages");
                    _logger.LogInformation($"------STOP  ADDING IMAGE TO CACHE. {context.Request.Path.Value}");
                }
            }
        }

        private void EnsureDestinationFolderExist(string directory)
        {
            if (!imageDirectoryExists)
            {
                var directoryName = Path.GetDirectoryName(directory);
                Directory.CreateDirectory(directoryName);
                imageDirectoryExists = true;
            }
        }

        private async Task WriteResponseBodyToDisk(Stream buffer, string destinationFile)
        {
            using (FileStream fs = new FileStream(destinationFile, FileMode.Create))
            {
                buffer.Seek(0, SeekOrigin.Begin);
                await buffer.CopyToAsync(fs);
            }
        }

        private void CleanupCachedFiles()
        {
            lock (_lockObject) //using (await _semaphoreSlim.UseWaitAsync()) //using (await _lock.LockAsync(imageCachingLockKey))
            {
                if (!filesToDelete.Any(f => f.Value < DateTime.UtcNow))
                    return;
                foreach (var filePath in filesToDelete.Where(f => f.Value < DateTime.UtcNow).Select(x => x.Key).ToList())
                { // delete expired file from disc
                    try
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                        _logger.LogInformation($"file {filePath} has been deleted");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"***** Unable to delete file {filePath}");
                        throw ex;
                    }
                    if (!filesToDelete.TryRemove(filePath, out DateTime outDate))
                        _logger.LogInformation($"***** Unable to remove file {filePath} from filesToDelete");
                }
            }
        }
    }
}
