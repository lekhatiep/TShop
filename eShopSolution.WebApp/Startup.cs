using eShopSolution.ApiIntegration;
using eShopSolution.ApiIntegration.Interface;
using eShopSolution.ViewModels.System.Users;
using eShopSolution.WebApp.LocalizationResources;
using FluentValidation.AspNetCore;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Net.Http;

namespace eShopSolution.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            var cultures = new[]
            {
                new CultureInfo("vi"),
                new CultureInfo("en"),
            };

            services.AddControllersWithViews()
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
            {
                ops.UseAllCultureProviders = false;
                ops.ResourcesPath = "LocalizationResources";
                ops.RequestLocalizationOptions = o =>
                {
                    o.SupportedCultures = cultures;
                    o.SupportedUICultures = cultures;
                    o.DefaultRequestCulture = new RequestCulture("vi");
                };
            })
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>()
                );

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/Index/";
                options.AccessDeniedPath = "/Forbiden";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ISlideApiClient, SlideApiClient>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ICategoryApiClient, CategoryApiClient>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IOrderApiClient, OrderApiClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseRequestLocalization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "Product Detail en",
                   pattern: "{culture=en}/products/{id}", new
                   {
                       controller = "Product",
                       action = "Detail"
                   });

                endpoints.MapControllerRoute(
                   name: "Product Detail vi",
                   pattern: "{culture=vi}/san-pham/{id}", new
                   {
                       controller = "Product",
                       action = "Detail"
                   });

                endpoints.MapControllerRoute(
                   name: "Product Category en",
                   pattern: "{culture=en}/categories/{id}", new
                   {
                       controller = "Product",
                       action = "Category"
                   });

                endpoints.MapControllerRoute(
                   name: "Product Detail vi",
                   pattern: "{culture=vi}/danh-muc/{id}", new
                   {
                       controller = "Product",
                       action = "Category"
                   });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=vi}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}