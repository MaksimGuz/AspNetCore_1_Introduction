using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BaseSiteWebApp.Interfaces;

namespace BaseSiteWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ISuppliersService _suppliersService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, ISuppliersService suppliersService)
        {                     
            _productsService = productsService;
            _categoriesService = categoriesService;
            _suppliersService = suppliersService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {            
            return View(await _productsService.GetAllAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetByIdAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = await _categoriesService.GetCategoriesSelectListAsync();
            ViewData["SupplierId"] = await _suppliersService.GetSuppliersSelectListAsync();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                await _productsService.Create(products);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = await _categoriesService.GetCategoriesSelectListAsync(products.CategoryId);
            ViewData["SupplierId"] = await _suppliersService.GetSuppliersSelectListAsync(products.SupplierId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetByIdAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = await _categoriesService.GetCategoriesSelectListAsync(products.CategoryId);
            ViewData["SupplierId"] = await _suppliersService.GetSuppliersSelectListAsync(products.SupplierId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = await _categoriesService.GetCategoriesSelectListAsync(products.CategoryId);
            ViewData["SupplierId"] = await _suppliersService.GetSuppliersSelectListAsync(products.SupplierId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetByIdAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _productsService.GetByIdAsync(id);
            await _productsService.Delete(products);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductsExists(int id)
        {
            return await _productsService.GetByIdAsync(id) == null;
        }
    }
}
