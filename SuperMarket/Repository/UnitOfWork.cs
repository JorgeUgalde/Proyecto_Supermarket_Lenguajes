using NuGet.Protocol.Core.Types;
using SuperMarket.Data;
using SuperMarket.Repository.Interfaces;

namespace SuperMarket.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IStoreRepository Store { get; private set; }

        public IOrderRepository Order { get; private set; }

        public ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductRepository = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
			Store = new StoreRepository(_db);
            Order = new OrderRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
