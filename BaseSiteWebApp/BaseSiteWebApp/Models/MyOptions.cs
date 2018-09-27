using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Models
{
    public class MyOptions
    {
        private int _maxProducts;

        public int MaxProducts {
            get => _maxProducts;
            set
            {
                Log.Information($"Read configuration. Value for MaxProducts: {value}");
                _maxProducts = value;
            }
        }
    }
}
