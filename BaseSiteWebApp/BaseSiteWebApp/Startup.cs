using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.Repositories;
using BaseSiteWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseSiteWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private IConfiguration _configuration { get; }
        private ILogger<Startup> _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(options => options.MaxModelValidationErrors = 50)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<NorthwindContext>(options => 
                options.UseSqlServer(_configuration.GetConnectionString("MyConnection")));
            _logger.LogInformation($"GET CONFIGURATION. MyConnection: {_configuration.GetConnectionString("MyConnection")}");
            services.Configure<MyOptions>(_configuration);
            _logger.LogInformation(@"GET CONFIGURATION. MyOptions: {@options}", _configuration.Get<MyOptions>());
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISuppliersRepository, SuppliersRepository>();
            services.AddTransient<ISuppliersService, SuppliersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            applicationLifetime.ApplicationStopped.Register(OnApplicationStopped);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "image",
                    template: "images/{id}",
                    defaults: new { controller = "Categories", action = "Image" });
            });
        }

        protected void OnApplicationStarted()
        {
            _logger.LogInformation($"Application has been started from {AppDomain.CurrentDomain.BaseDirectory} folder");
        }

        protected void OnApplicationStopping()
        {
            _logger.LogInformation($"Application is stopping");
        }

        protected void OnApplicationStopped()
        {
            _logger.LogInformation($"Application has been stopped");
        }
    }
}
