using SuperMarket.Data;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;

namespace SuperMarket.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            _db.Update(order);
        }
    }
}
