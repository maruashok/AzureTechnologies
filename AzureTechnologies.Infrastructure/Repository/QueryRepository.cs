using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AzureTechnologies.Domain.Interfaces.Repositiry;

namespace AzureTechnologies.Infrastructure.Repository
{
    public class QueryRepository<TEntity> : IDisposable, IQueryRepository<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private readonly DbContext dBContext;
        private readonly DbSet<TEntity> dbSet;

        public QueryRepository(DbContext context)
        {
            dBContext = context;
            dbSet = context.Set<TEntity>();
        }

        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? filter = null, bool asNoTracking = true)
        {
            IQueryable<TEntity> query = asNoTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<TEntity?> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return GetQueryable().AnyAsync(filter);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dBContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}