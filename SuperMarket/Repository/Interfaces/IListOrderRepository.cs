using SuperMarket.Models;

namespace SuperMarket.Repository.Interfaces
{
    public interface IListOrderRepository : IRepository<ProductOrder>
    {
        void Update(ProductOrder productOrder);
    }
}
