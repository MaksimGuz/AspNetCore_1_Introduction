using BaseSiteWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class ApiSuppliersController : Controller
    {
        private ISuppliersService _suppliersService;

        public ApiSuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _suppliersService.GetAllAsync());
        }

    }
}
