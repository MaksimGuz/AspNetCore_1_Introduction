﻿using BaseSiteWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Services
{
    public class SuppliersService : ISuppliersService
    {
        private ISuppliersRepository _suppliersRepository;

        public SuppliersService(ISuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        public async Task<SelectList> GetSuppliersSelectListAsync(int? id = null)
        {
            return new SelectList( await _suppliersRepository.GetAllAsync(), "SupplierId", "CompanyName", id);
        }
    }
}
