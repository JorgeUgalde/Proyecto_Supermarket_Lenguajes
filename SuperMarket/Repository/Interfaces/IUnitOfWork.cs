namespace SuperMarket.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository Category { get; }

        IStoreRepository Store { get; }

        IOrderRepository Order { get; }

        void Save();
    }
}
