using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Repositories
{
    public class SuppliersRepository : RepositoryAsync<Suppliers>, ISuppliersRepository
    {
        public SuppliersRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}
