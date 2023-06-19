using SuperMarket.Data;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;

namespace SuperMarket.Repository
{
	public class StoreRepository : Repository<Store>,IStoreRepository
	{
		private ApplicationDbContext _db;

		public StoreRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Store store)
		{
			_db.Update(store);
		}
	}
}
