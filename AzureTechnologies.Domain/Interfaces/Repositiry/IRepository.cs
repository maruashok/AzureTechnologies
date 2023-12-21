namespace AzureTechnologies.Domain.Interfaces.Repositiry
{
    public interface IRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}