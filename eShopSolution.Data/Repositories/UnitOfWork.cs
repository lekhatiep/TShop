using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Repositories.Interface;
using System;
using System.Threading.Tasks;

namespace eShopSolution.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EShopDbContext context;

        //public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<Slide> SlideRepository { get; }

        public UnitOfWork(EShopDbContext context,
           // IGenericRepository<Order> OrderRepository,
            IGenericRepository<Slide> SlideRepository
            )
        {
            this.context = context;
            //this.OrderRepository = OrderRepository;
            this.SlideRepository = SlideRepository;
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