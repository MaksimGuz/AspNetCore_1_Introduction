using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSiteWebApp.Filters;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Middleware;
using BaseSiteWebApp.Models;
using BaseSiteWebApp.Repositories;
using BaseSiteWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));
            services.Configure<MyLoggingFilterOptions>(_configuration.GetSection("MyLoggingFilterOptions"));
            _logger.LogInformation(@"GET CONFIGURATION. MyLoggingFilterOptions: {@options}", _configuration.GetSection("MyLoggingFilterOptions").Get<MyLoggingFilterOptions>());
            services.AddMvc(options => { options.MaxModelValidationErrors = 50; options.Filters.Add(new MyLoggingFilter(_logger, _configuration.GetSection("MyLoggingFilterOptions").Get<MyLoggingFilterOptions>())); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDbContext<NorthwindContext>(options => 
                options.UseSqlServer(_configuration.GetConnectionString("MyConnection")));
            _logger.LogInformation($"GET CONFIGURATION. MyConnection: {_configuration.GetConnectionString("MyConnection")}");
            services.Configure<ProductOptions>(_configuration.GetSection("ProductOptions"));
            services.Configure<ImageCachingOptions>(_configuration.GetSection("ImageCachingOptions"));
            _logger.LogInformation(@"GET CONFIGURATION. ProductOptions: {@options}", _configuration.GetSection("ProductOptions").Get<ProductOptions>());
            _logger.LogInformation(@"GET CONFIGURATION. ImageCachingOptions: {@options}", _configuration.GetSection("ImageCachingOptions").Get<ImageCachingOptions>());
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISuppliersRepository, SuppliersRepository>();
            services.AddTransient<ISuppliersService, SuppliersService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IEmailSender, AuthMessageSender>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Mentoring API", Version = "v1" });
            });
            return services.BuildServiceProvider();
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseImageCaching();
            app.UseSpaStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mentoring API V1");
            });
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "image",
                   template: "images/{id}",
                   defaults: new { controller = "Categories", action = "Image" });                
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action?}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
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
