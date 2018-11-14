using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Services
{
    public class AdministratorService : IAdministratorService
    {
        private const string adminRole = "Administrator";
        private IServiceProvider _serviceProvider;
        private UserManager<IdentityUser> _userManager;

        public AdministratorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        }
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var tasks = users.Select(async user => new UserViewModel
            {
                UserName = user.UserName,
                IsAdmin = await _userManager.IsInRoleAsync(user, adminRole)
            });
            return await Task.WhenAll(tasks);
        }

        public async Task<UserViewModel> GetByNameAsync(string userName)
        {            
            return new UserViewModel { UserName = (await _userManager.FindByNameAsync(userName)).UserName };
        }

        public async Task MakeAdminAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return;
            var isAdmin = await _userManager.IsInRoleAsync(user, adminRole);
            if (isAdmin)
                return;
            await _userManager.AddToRoleAsync(user, adminRole);
        }
    }
}
