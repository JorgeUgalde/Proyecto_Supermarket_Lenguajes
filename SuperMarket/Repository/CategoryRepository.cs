using SuperMarket.Data;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using System.Linq.Expressions;

namespace SuperMarket.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Update (category);
        }
    }
}
