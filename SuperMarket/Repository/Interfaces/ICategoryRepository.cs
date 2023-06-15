using SuperMarket.Models;

namespace SuperMarket.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
