using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Repositories
{
    public class CategoriesRepository : RepositoryAsync<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(NorthwindContext dbContext) : base (dbContext)
        {
        }
    }
}
