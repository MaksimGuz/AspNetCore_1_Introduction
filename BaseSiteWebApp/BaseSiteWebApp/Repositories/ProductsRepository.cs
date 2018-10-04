using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseSiteWebApp.Repositories
{
    public class ProductsRepository : RepositoryAsync<Products>, IProductsRepository
    {
        public ProductsRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}
