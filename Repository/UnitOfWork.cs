using TShopSolution.Data.EF;
using TShopSolution.Data.Entities;
using System;
using System.Threading.Tasks;
using tShop.Repository.Interface;

namespace tShop.Repository
{
    public class UnitOfWork
    {
        private readonly EShopDbContext context;

        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<Slide> SlideRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<CategoryTranslation> CategoryTranslationRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<ProductTranslation> ProductTranslationRepository { get; }
        public IGenericRepository<ProductImage> ProductImageRepository { get; }
        public IGenericRepository<ProductInCategory> ProductInCategoryRepository { get; }
        public IGenericRepository<Language> LanguageRepository { get; }

        public UnitOfWork(EShopDbContext context,
            IGenericRepository<Order> OrderRepository,
            IGenericRepository<Slide> SlideRepository,
            IGenericRepository<Category> CategoryRepository,
            IGenericRepository<CategoryTranslation> CategoryTranslationRepository,
            IGenericRepository<Product> ProductRepository,
            IGenericRepository<ProductTranslation> ProductTranslationRepository,
            IGenericRepository<ProductImage> ProductImageRepository,
            IGenericRepository<ProductInCategory> ProductInCategoryRepository,
            IGenericRepository<Language> LanguageRepository

            )
        {
            this.context = context;
            this.OrderRepository = OrderRepository;
            this.SlideRepository = SlideRepository;
            this.CategoryRepository = CategoryRepository;
            this.CategoryTranslationRepository = CategoryTranslationRepository;
            this.ProductRepository = ProductRepository;
            this.ProductTranslationRepository = ProductTranslationRepository;
            this.ProductImageRepository = ProductImageRepository;
            this.ProductInCategoryRepository = ProductInCategoryRepository;
            this.LanguageRepository = LanguageRepository;
        }

        public int SaveChanges()
        {
            var iResult = context.SaveChanges();
            return iResult;
        }

        public async Task<int> SaveChangesAsync()
        {
            var iResult = await context.SaveChangesAsync();
            return iResult;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}