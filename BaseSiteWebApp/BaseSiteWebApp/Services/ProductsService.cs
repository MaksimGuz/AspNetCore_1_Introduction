using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseSiteWebApp.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productsRepository;
        private ProductOptions _options;

        public ProductsService(IProductsRepository ProductsRepository, IOptions<ProductOptions> optionsAccessor)
        {
            _productsRepository = ProductsRepository;
            _options = optionsAccessor.Value;
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _productsRepository.GetAllAsync(source => source.Include(p => p.Category).Include(p => p.Supplier), _options.MaxProducts);
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _productsRepository.GetSingleAsync(source => source.ProductId == id, source => source.Include(p => p.Category).Include(p => p.Supplier));
        }

        public async Task Update(Products products)
        {
            await _productsRepository.Update(products);
        }

        public async Task Create(Products products)
        {
            await _productsRepository.Create(products);
        }

        public async Task Delete(Products products)
        {
            await _productsRepository.Delete(products);
        }
    }
}