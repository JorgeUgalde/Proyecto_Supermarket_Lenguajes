using SuperMarket.Models;

namespace SuperMarket.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
    }
}
