using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BaseSiteWebApp.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = GetItems();
            return View(items);
        }
        private List<string> GetItems()
        {
            List<string> result = new List<string> { "Home" };
            try
            {   
                var items = new List<string>();
                if (HttpContext.Request?.Path.HasValue == true)
                    items = HttpContext.Request.Path.Value.Substring(1).Split("/").ToList();
                if (items?.Count() > 0)
                {
                    for (int i = 0; i < items.Count() && i < 2; i++)
                        result.Add(items[i]);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
