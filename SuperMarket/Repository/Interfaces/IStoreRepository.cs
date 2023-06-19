using SuperMarket.Models;

namespace SuperMarket.Repository.Interfaces
{
	public interface IStoreRepository : IRepository<Store>
	{
		void Update(Store store);
	}
}
