public interface IUnitOfWork
{
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}