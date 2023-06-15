namespace SuperMarket.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }

        ICategoryRepository Category { get; }

        void Save();
    }
}
