using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.EntityFrameworkCore.Internal;
using System.Globalization;
using System.Threading;

namespace BaseSiteWebApp.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ImageCachingOptions _imageCachingOptions;
        private readonly ILogger<ImageCachingMiddleware> _logger;
        private ConcurrentDictionary<string, CachedImageInfo> cachedImages = new ConcurrentDictionary<string, CachedImageInfo>();
        private ConcurrentDictionary<string, CachedImageInfo> filesToDelete = new ConcurrentDictionary<string, CachedImageInfo>();
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private bool imageDirectoryExists = false;

        public ImageCachingMiddleware(RequestDelegate next, IOptions<ImageCachingOptions> optionsAccessor, ILogger<ImageCachingMiddleware> logger)
        {
            _next = next;
            _imageCachingOptions = optionsAccessor.Value;
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
            _logger.LogInformation($"Image caching middleware finish. {context.Request.Path.Value}");

            if (IsRequestToCategoriesImages(context))
            {
                try
                {
                    // reset the buffer and retrieve the content
                    buffer.Seek(0, SeekOrigin.Begin);
                    var responseBody = await reader.ReadToEndAsync();

                    await AddImageToCache(context, buffer);
                    ProcessFilesToDelete();

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
        }
        private bool IsRequestToCategoriesImages(HttpContext context)
        {
            return context != null && context.Response != null && context.Request.Path != null &&
                context.Request.Path.Value.StartsWith("/Categories/Image/");
        }

        private CachedImageInfo GetCachedImage(string requestPath)
        {
            cachedImages.TryGetValue(requestPath, out CachedImageInfo image);
            return image;
        }

        private async Task<bool> FillResponseStreamFromCache(HttpContext context, Stream responseStream, string requestPath)
        {
            if (cachedImages.TryGetValue(requestPath, out CachedImageInfo image))
            {
                if (image.Expires > DateTime.UtcNow)
                {
                    using (new ImageCacheItemLock(image.FilePath)) //using(await _lock.UseWaitAsync())
                    {
                        _logger.LogInformation($"------START GETTING IMAGE FROM CACHE. {context.Request.Path.Value}");
                        context.Response.ContentType = image.ContentType;
                        if (!File.Exists(image.FilePath))
                            return false;
                        using (var fileStream = new FileStream(image.FilePath, FileMode.Open))
                        {
                            await fileStream.CopyToAsync(responseStream);
                        }
                        _logger.LogInformation($"Image from cache for request {context.Request.Path.Value}");
                        context.Response.Body = responseStream;
                        _logger.LogInformation($"------STOP  GETTING IMAGE FROM CACHE. {context.Request.Path.Value}");
                        return true;
                    }
                }
            }
            return false;
        }

        private async Task AddImageToCache(HttpContext context, Stream buffer)
        {
            using (await _lock.UseWaitAsync())
            {
                if (cachedImages.Any(f => f.Value.Expires < DateTime.UtcNow))
                {
                    Parallel.ForEach(cachedImages.Where(f => f.Value.Expires < DateTime.UtcNow).Select(x => x.Value),
                        new ParallelOptions() { MaxDegreeOfParallelism = _imageCachingOptions.MaxDegreeOfParallelism },
                        imageItem =>
                        {
                            filesToDelete.TryAdd(imageItem.FilePath, imageItem);
                            cachedImages.TryRemove(context.Request.Path, out CachedImageInfo imageOut);
                        });
                }
            }
            if (cachedImages.Count() == _imageCachingOptions.MaxImages)
                return;

            if (!cachedImages.TryGetValue(context.Request.Path, out CachedImageInfo image))
            {
                string filePath = $"{_imageCachingOptions.CacheDirectoryPath}{context.Request.Path.Value.Replace("/", "")}{DateTime.UtcNow.ToString("_yyyy_mm_dd_hh_mm", CultureInfo.InvariantCulture)}.bmp";
                using (new ImageCacheItemLock(filePath)) //using(await _lock.UseWaitAsync())
                {
                    _logger.LogInformation($"------START ADDING IMAGE TO CACHE. {context.Request.Path.Value}");
                    EnsureDestinationFolderExist(_imageCachingOptions.CacheDirectoryPath);
                    await WriteBodyToDisk(buffer, filePath);
                    _logger.LogInformation($"Image {filePath} saved to disk. {context.Request.Path.Value}");
                    cachedImages.TryAdd(context.Request.Path, new CachedImageInfo(filePath, context.Response.ContentType, _imageCachingOptions));
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

        private async Task WriteBodyToDisk(Stream buffer, string destinationFile)
        {
            using (FileStream fs = new FileStream(destinationFile, FileMode.Create))
            {
                buffer.Seek(0, SeekOrigin.Begin);
                await buffer.CopyToAsync(fs);
            }
        }

        private void ProcessFilesToDelete()
        {
            if (!filesToDelete.Any(f => f.Value.CanBeDeleted()))
                return;
            Parallel.ForEach(filesToDelete.Where(f => f.Value.CanBeDeleted()).Select(x=> x.Key),
                new ParallelOptions() { MaxDegreeOfParallelism = _imageCachingOptions.MaxDegreeOfParallelism },
                filePath =>
                {
                    using (new ImageCacheItemLock(filePath))
                    {
                        // delete expired file from disc
                        try
                        {
                            if (File.Exists(filePath))
                                File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "UNABLE TO DELETE FILE");
                            throw ex;
                        }
                    }
                    filesToDelete.TryRemove(filePath, out CachedImageInfo image);
                });
        }
    }
}
