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
        private ConcurrentDictionary<string, CachedImageInfo> filesToDelete = new ConcurrentDictionary<string, CachedImageInfo>();
        private bool imageDirectoryExists = false;
        private readonly StripedAsyncLock<string> _lock = null;
        //private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

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
                LogImageCache(context.Request.Path.Value);
                LogImageToDeleteCache(context.Request.Path.Value);
                if (await FillResponseStreamFromCache(context, responseStream))
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

                    LogImageCache(context.Request.Path.Value);
                    LogImageToDeleteCache(context.Request.Path.Value);
                    await AddImageToCache(context, buffer);
                    await CleanupCachedFiles(context.Request.Path.Value);
                    LogImageCache(context.Request.Path.Value);
                    LogImageToDeleteCache(context.Request.Path.Value);

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

        private async Task<bool> FillResponseStreamFromCache(HttpContext context, Stream responseStream)
        {
            using (await _lock.LockAsync(context.Request.Path.Value)) //using (new ImageCacheItemLock(image.FilePath)) //using(await _lock.UseWaitAsync())
            {
                if (cachedImages.TryGetValue(context.Request.Path.Value, out CachedImageInfo image))
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
            using (await _lock.LockAsync(context.Request.Path.Value))
            {
                if (cachedImages.TryGetValue(context.Request.Path.Value, out CachedImageInfo imgBefore))
                {
                    if (imgBefore.Expires < DateTime.UtcNow)
                    {
                        if (!cachedImages.TryRemove(context.Request.Path.Value, out CachedImageInfo imageOut))
                            _logger.LogInformation($"***** file {imgBefore.FilePath} was not removed from cachedImages for request {context.Request.Path.Value}");
                        else
                        {
                            _logger.LogInformation($"***** file {imgBefore.FilePath} was succesfully removed from cachedImages for request {context.Request.Path.Value}");
                            if (!filesToDelete.TryAdd(imgBefore.FilePath, imageOut))
                                _logger.LogInformation($"***** file {imgBefore.FilePath} was not added to filesToDelete for request {context.Request.Path.Value}");
                            else
                                _logger.LogInformation($"***** file {imgBefore.FilePath} was succesfully added to filesToDelete for request {context.Request.Path.Value}");
                        }
                    }
                }
                if (cachedImages.Count() >= _imageCachingOptions.MaxImages)
                    return; // cache is full

                if (!cachedImages.TryGetValue(context.Request.Path.Value, out CachedImageInfo image))
                {
                    string filePath = $"{_imageCachingOptions.CacheDirectoryPath}{context.Request.Path.Value.Replace("/", "")}{DateTime.UtcNow.ToString("_yyyy_mm_dd_hh_mm", CultureInfo.InvariantCulture)}.bmp";
                    _logger.LogInformation($"------START ADDING IMAGE TO CACHE. {context.Request.Path.Value}");
                    EnsureDestinationFolderExist(_imageCachingOptions.CacheDirectoryPath);
                    await WriteResponseBodyToDisk(buffer, filePath);
                    _logger.LogInformation($"Image {filePath} saved to disk. {context.Request.Path.Value}");
                    if (!cachedImages.TryAdd(context.Request.Path.Value, new CachedImageInfo(filePath, context.Response.ContentType, _imageCachingOptions, context.Request.Path.Value)))
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

        private async Task CleanupCachedFiles(string requestPath)
        {
            using (await _lock.LockAsync(requestPath))
            {
                if (!filesToDelete.Any(f => f.Value.RequestPath == requestPath && f.Value.ExpiresForDeleteFile < DateTime.UtcNow))
                    return;
                foreach (var imageToDelete in filesToDelete.Where(f => f.Value.RequestPath == requestPath && f.Value.ExpiresForDeleteFile < DateTime.UtcNow).Select(x => x.Value).ToList())
                { // delete expired file from disc
                    try
                    {
                        if (File.Exists(imageToDelete.FilePath))
                            File.Delete(imageToDelete.FilePath);
                        _logger.LogInformation($"file {imageToDelete.FilePath} has been deleted");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"***** Unable to delete file {imageToDelete.FilePath}");
                        throw ex;
                    }
                    if (!filesToDelete.TryRemove(imageToDelete.FilePath, out  CachedImageInfo outImage))
                        _logger.LogInformation($"***** Unable to remove file {imageToDelete.FilePath} from filesToDelete. {requestPath}");
                }
            }
        }

        private void LogImageCache(string param)
        {
            foreach (var item in cachedImages.ToList())
                _logger.LogInformation(@"cachedImages item: {@item} from {@param}", item.Value, param);
        }
        private void LogImageToDeleteCache(string param)
        {
            foreach (var item in filesToDelete.ToList())
                _logger.LogInformation(@"filesToDelete key: {@key} item value: {@value} from {param}", item.Key, item.Value, param);
        }
    }
}
