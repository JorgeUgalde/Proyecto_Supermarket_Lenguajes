namespace SuperMarket.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository Category { get; }

        void Save();
    }
}
