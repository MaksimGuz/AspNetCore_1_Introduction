using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BaseSiteWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _emailSender;
        private ILogger<HomeController> _logger;

        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome to the web app.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            _logger.LogError(error, "RequestId:{requestId}, error: {@error}", requestId, error);
            return View(new ErrorViewModel { RequestId = requestId });
        }

        [Authorize]
        public async Task<IActionResult> TestSendEmail()
        {            
            await _emailSender.SendEmailAsync("maksim.guz@gmail.com", "test subject",
                        $"<h1>email body</h1>");
            ViewData["Message"] = "Email has been sent";
            return View();
        }
    }
}
