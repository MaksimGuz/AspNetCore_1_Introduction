using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseSiteWebApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSiteWebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _administratorService.GetAllUsersAsync();
            return View(users);
        }
        
        public async Task<IActionResult> MakeAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var userViewModel = await _administratorService.GetByNameAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        [HttpPost, ActionName("MakeAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAdminConfirmed(string username)
        {
            await _administratorService.MakeAdminAsync(username);
            return RedirectToAction(nameof(Index));
        }
    }
}