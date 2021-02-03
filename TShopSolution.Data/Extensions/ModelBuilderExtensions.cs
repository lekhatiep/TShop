using TShopSolution.Data.Entities;
using TShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is homepage eshop" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword eshop" },
                new AppConfig() { Key = "HomeDescription", Value = "This is Description eshop" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, ParentId = null, IsShowOnHome = true, SortOrDer = 1, Status = Status.Active },
                new Category() { Id = 2, ParentId = null, IsShowOnHome = true, SortOrDer = 1, Status = Status.Active }
                );

            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    LanguageId = "vi",
                    Name = "Áo sơ mi",
                    SeoAlias = "ao-so-mi",
                    SeoDescription = "Áo sơ mi"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    LanguageId = "en",
                    Name = "Shirt",
                    SeoAlias = "shirt",
                    SeoDescription = "Shirt"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    LanguageId = "vi",
                    Name = "Giầy thể thao",
                    SeoAlias = "giay-the-thao",
                    SeoDescription = "Giầy thể thao"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    LanguageId = "en",
                    Name = "sport shoes",
                    SeoAlias = "sport-shoes",
                    SeoDescription = "sport shoes"
                }

                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    Original = 10000,
                    Price = 20000,
                    Stock = 0,
                    ViewCount = 0,
                    IsFeatured = true,
                });
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    LanguageId = "vi",
                    Name = "Áo sơ mi TK",
                    SeoAlias = "ao-so-mi-tk",
                    SeoTitle = "Áo sơ mi TK",
                    SeoDescription = "Áo sơ mi TK",
                    Details = "Áo sơ mi TK",
                    Description = "Áo sơ mi TK"
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    LanguageId = "en",
                    Name = "TK Shirt",
                    SeoAlias = "tk-shirt",
                    SeoTitle = "TK Shirt",
                    SeoDescription = "TK Shirt",
                    Details = "TK Shirt",
                    Description = "TK Shirt"
                });

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory { CategoryId = 1, ProductId = 1 }
                );

            // any guid
            var ADMIN_ID = new Guid("55163ED5-C8DB-474F-8ED7-7DE08F1A31A3");
            // any guid, but nothing is against to use the same one
            var ROLE_ID = new Guid("09A70B54-9435-4FEE-8335-C23873743C84");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator cho app"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tieplk@gmail.com",
                NormalizedEmail = "tieplk@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc123456@"),
                SecurityStamp = string.Empty,
                FirstName = "Tiep",
                LastName = "Le",
                DoB = new DateTime(2020, 06, 22)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            modelBuilder.Entity<Slide>().HasData(
               new Slide() { Id = 1, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/1.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 1, Status = Status.Active },
               new Slide() { Id = 2, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/2.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 2, Status = Status.Active },
               new Slide() { Id = 3, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/3.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 3, Status = Status.Active },
               new Slide() { Id = 4, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/4.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 4, Status = Status.Active },
               new Slide() { Id = 5, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/5.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 5, Status = Status.Active },
                new Slide() { Id = 6, Url = "#", Name = "Second Thumbnail label", Image = "/themes/images/carousel/5.png", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus.", SortOrder = 6, Status = Status.Active }
               );
        }
    }
}