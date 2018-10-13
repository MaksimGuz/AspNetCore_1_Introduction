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
        private ConcurrentDictionary<string, CachedFileToDelete> filesToDelete = new ConcurrentDictionary<string, CachedFileToDelete>();
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

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
                   
                    // copy back our buffer to the response stream
                    buffer.Seek(0, SeekOrigin.Begin);
                    await buffer.CopyToAsync(responseStream);
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
            await _lock.WaitAsync(); //using (new ImageCacheItemLock(filePath))
            try
            {
                if (cachedImages.TryGetValue(requestPath, out CachedImageInfo image))
                {
                    if (image.Expires > DateTime.UtcNow)
                    {
                        context.Response.ContentType = image.ContentType;
                        using (var fileStream = new FileStream(image.FilePath, FileMode.Open))
                        {
                            await fileStream.CopyToAsync(responseStream);
                        }
                        _logger.LogInformation($"Image from cache for request {context.Request.Path.Value}");
                        context.Response.Body = responseStream;
                        return true;
                    }
                }
            }
            finally
            {
                _lock.Release();
            }
            return false;
        }

        private async Task AddImageToCache(HttpContext context, Stream buffer)
        {
            await _lock.WaitAsync();
            try
            {
                EnsureDestinationFolderExist(_imageCachingOptions.CacheDirectoryPath);
                string filePath = $"{_imageCachingOptions.CacheDirectoryPath}{context.Request.Path.Value.Replace("/", "")}{DateTime.UtcNow.ToString("_yyyy_mm_dd_hh_mm_ss", CultureInfo.InvariantCulture)}.bmp";
                cachedImages.TryGetValue(context.Request.Path, out CachedImageInfo image);
                if (image == null || image.Expires < DateTime.UtcNow || image.ContentType != context.Response.ContentType)
                {
                    await WriteBodyToDisk(buffer, filePath);
                    _logger.LogInformation($"Image saved to disk. {context.Request.Path.Value}");
                    if (image != null)
                    { // delete expired file from disc
                        try
                        {
                            if (File.Exists(filePath))
                                File.Delete(image.FilePath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "UNABLE TO DELETE FILE");
                            throw ex;
                        }
                    }
                    cachedImages.AddOrUpdate(context.Request.Path,
                        new CachedImageInfo
                        {
                            ContentType = context.Response.ContentType,
                            Expires = DateTime.UtcNow.AddSeconds(_imageCachingOptions.CacheExpirationInSeconds),
                            FilePath = filePath
                        },
                        (key, img) =>
                        {
                            img.ContentType = context.Response.ContentType;
                            img.Expires = DateTime.UtcNow.AddSeconds(_imageCachingOptions.CacheExpirationInSeconds);
                            img.FilePath = filePath;
                            return img;
                        }
                    );
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        private void EnsureDestinationFolderExist(string destinationFile)
        {
            var directoryName = Path.GetDirectoryName(destinationFile);
            Directory.CreateDirectory(directoryName);
        }

        private async Task WriteBodyToDisk(Stream buffer, string destinationFile)
        {
            using (FileStream fs = new FileStream(destinationFile, FileMode.Create))
            {
                buffer.Seek(0, SeekOrigin.Begin);
                await buffer.CopyToAsync(fs);
            }
        }
    }
}
