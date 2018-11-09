using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class ApiProductsController : Controller
    {
        private readonly ILogger<ApiProductsController> _logger;
        private readonly IProductsService _productsService;

        public ApiProductsController(IProductsService productsService, ILogger<ApiProductsController> logger)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]Products products)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid owner object sent from client.");
                return BadRequest("Invalid model object");
            }
            else
            {
                await _productsService.Create(products);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm]Products products)
        {
            if (id != products.ProductId)
            {
                _logger.LogError("Id from Url and model doesn't match.");
                return BadRequest("Id from Url and model doesn't match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.Update(products);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductsExists(products.ProductId) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _logger.LogError("Invalid owner object sent from client.");
                return BadRequest("Invalid model object");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productsService.Delete(id);
            return NoContent();
        }

        private async Task<bool> ProductsExists(int id)
        {
            return await _productsService.GetByIdAsync(id) == null;
        }
    }
}
