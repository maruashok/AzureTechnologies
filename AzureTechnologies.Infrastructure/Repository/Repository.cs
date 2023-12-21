using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AzureTechnologies.Domain.Interfaces.Repositiry;

namespace AzureTechnologies.Infrastructure.Repository
{
    public class Repository<TEntity> : QueryRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dBContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DbContext context) : base(context)
        {
            dBContext = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity != null)
            {
                await dbSet.AddAsync(entity);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return dBContext.SaveChangesAsync();
        }
    }
}