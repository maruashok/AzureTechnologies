using System.Linq.Expressions;

namespace AzureTechnologies.Domain.Interfaces.Repositiry
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        void Dispose();

        Task<TEntity?> GetByIDAsync(object id);

        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}