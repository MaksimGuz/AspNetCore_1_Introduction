using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Interfaces
{
    public interface IAdministratorService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> GetByNameAsync(string userName);
        Task MakeAdminAsync(string userName);
    }
}
