using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Interfaces
{
    public interface ISuppliersService
    {
        Task<SelectList> GetSuppliersSelectListAsync(int? id = null);
    }
}
