using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BaseSiteWebApp.Models;

namespace BaseSiteWebApp.Filters
{
    public class MyLoggingFilter : IAsyncActionFilter
    {
        private ILogger _logger;
        private MyLoggingFilterOptions _myLoggingFilterOptions;

        public MyLoggingFilter(ILogger logger, MyLoggingFilterOptions myLoggingFilterOptions)
        {
            _logger = logger;
            _myLoggingFilterOptions = myLoggingFilterOptions;
        }
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            _logger.LogInformation($" ### START {context?.Controller?.GetType()}");
            if (_myLoggingFilterOptions.LogParams)
                _logger.LogInformation($" ### {context?.Controller?.GetType()}. Arguments: {JsonConvert.SerializeObject(context?.ActionArguments)}");
            var resultContext = await next();
            _logger.LogInformation($" ### END {context?.Controller?.GetType()}");
        }
    }
}
