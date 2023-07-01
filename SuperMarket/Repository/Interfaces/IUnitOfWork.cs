namespace SuperMarket.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository Category { get; }
        IStoreRepository Store { get; }
        IOrderRepository Order { get; }
        IListOrderRepository ListOrder { get; }
        IApplicationUserRepository ApplicationUser { get; }

        void Save();
    }
}
