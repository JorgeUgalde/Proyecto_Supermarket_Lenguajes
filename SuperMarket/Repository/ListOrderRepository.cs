using SuperMarket.Data;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using System.Linq.Expressions;

namespace SuperMarket.Repository
{
    public class ListOrderRepository : Repository<ProductOrder>, IListOrderRepository
    {
        private ApplicationDbContext _db;

        public ListOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductOrder productOrder)
        {
            _db.Update (productOrder);
        }
    }
}
