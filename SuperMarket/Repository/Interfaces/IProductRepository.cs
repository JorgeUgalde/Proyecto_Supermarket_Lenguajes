using SuperMarket.Models;

namespace SuperMarket.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
