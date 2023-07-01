using SuperMarket.Data;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;

namespace SuperMarket.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            _db.Update(applicationUser);
        }
    }
}
