using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Middleware
{
    public class InitAdmiRoleForTheFirstUserMiddleware
    {
        private const string adminRole = "Administrator";
        private bool adminExists = false;        
        private RequestDelegate _next;
        private ILogger<InitAdmiRoleForTheFirstUserMiddleware> _logger;
        private IServiceProvider _serviceProvider;
        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public InitAdmiRoleForTheFirstUserMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<InitAdmiRoleForTheFirstUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
    }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (!adminExists)
            {
                using (await _lock.UseWaitAsync())
                {
                    var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    if (userManager.Users.Count() == 0)
                        return;
                    var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    //Check that there is an Administrator role and create if not
                    var hasAdminRole = await roleManager.RoleExistsAsync(adminRole);
                    if (!hasAdminRole)
                        await roleManager.CreateAsync(new IdentityRole(adminRole));
                    var adminUsers = await userManager.GetUsersInRoleAsync(adminRole);
                    if (adminUsers?.Count == 0)
                    {
                        var firstUser = userManager.Users.FirstOrDefault();
                        if (firstUser == null)
                            return;
                        await userManager.AddToRoleAsync(firstUser, adminRole);
                        _logger.LogInformation($"User {firstUser.UserName} has been added into {adminRole} role");
                    }
                    adminExists = true;
                }
            }
        }
    }
}
