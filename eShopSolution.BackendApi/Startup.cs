using eShopSolution.Application.Catolog.Category;
using eShopSolution.Application.Catolog.Orders;
using eShopSolution.Application.Catolog.Products;
using eShopSolution.Application.Common;
using eShopSolution.Application.Common.Slide;
using eShopSolution.Application.System.Auth;
using eShopSolution.Application.System.Languages;
using eShopSolution.Application.System.Roles;
using eShopSolution.Application.System.Users;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constant;
using eShopSolution.ViewModels.System.Users;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using tShop.Repository;
using tShop.Repository.Interface;

namespace eShopSolution.BackendApi
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
            services.AddDbContext<EShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstant.MainConnectionString)));
            //Register Identity for Token
            services.AddIdentity<AppUser, AppRole>().
                AddEntityFrameworkStores<EShopDbContext>().
                AddDefaultTokenProviders();
            //Declare DI and instance for Controller
            services.AddTransient<IStrorageService, FileStorageService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<ITokenRefresh, TokenRefresh>();
            services.AddTransient<IRefreshTokenGenerate, RefreshTokenGenerate>();

            //Add Regiser Repository Entity
            services.AddTransient<IGenericRepository<Slide>, GenericRepository<Slide>>();
            services.AddTransient<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IGenericRepository<CategoryTranslation>, GenericRepository<CategoryTranslation>>();
            services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddTransient<IGenericRepository<ProductInCategory>, GenericRepository<ProductInCategory>>();
            services.AddTransient<IGenericRepository<ProductImage>, GenericRepository<ProductImage>>();
            services.AddTransient<IGenericRepository<ProductTranslation>, GenericRepository<ProductTranslation>>();
            services.AddTransient<IGenericRepository<Language>, GenericRepository<Language>>();
            services.AddTransient<UnitOfWork>();

            //Register Validator each
            //services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            //services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            //Register configuration and validate token
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                //ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
            //Register Validator all project
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " API eShop Solution",
                    Description = "A simple example ASP.NET Core Web API",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme  = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger tShop API Version 0.1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}