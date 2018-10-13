using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Middleware
{
    public static class ImageCachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageCaching(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageCachingMiddleware>();
        }
    }
}
