using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BaseSiteWebApp.ApiControllers
{
    [Route("api/[controller]")]
    public class ApiCategoriesController : Controller
    {
        private ICategoriesService _categoriesService;
        private ILogger<ApiCategoriesController> _logger;

        public ApiCategoriesController(ICategoriesService categoriesService, ILogger<ApiCategoriesController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoriesService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var model = await _categoriesService.GetCategoriesEditImageViewModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(new { CategoryId = model.CategoryId, CategoryName = model.CategoryName });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm]CategoriesEditImageViewModel model)
        {
            try
            {
                if (id != model.CategoryId)
                {
                    _logger.LogError("Id from Url and model doesn't match.");
                    return BadRequest("Id from Url and model doesn't match.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                try
                {
                    await _categoriesService.UpdateFromCategoriesEditImageViewModel(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await CategoriesExists(model.CategoryId) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        private async Task<bool> CategoriesExists(int id)
        {
            return await _categoriesService.GetByIdAsync(id) == null;
        }
    }
}
