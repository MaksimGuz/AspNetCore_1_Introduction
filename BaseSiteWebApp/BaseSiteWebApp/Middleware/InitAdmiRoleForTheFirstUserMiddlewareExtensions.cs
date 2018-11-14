using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Middleware
{
    public static class InitAdmiRoleForTheFirstUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseInitAdmiRoleForTheFirstUser(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InitAdmiRoleForTheFirstUserMiddleware>();
        }
    }
}
