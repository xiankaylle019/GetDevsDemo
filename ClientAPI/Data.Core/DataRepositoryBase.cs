using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ClientAPI.Data.Core
{
    public abstract class DataRepositoryBase<TEntity, TContext>: DesignTimeDbContextFactory<TContext>,IDataRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
    {
        /// <summary>
        /// Async abstract get entity by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract Task<TEntity> GetEntityById(TContext context,  int id);
        /// <summary>
        /// Get DbContext instance
        /// </summary>
        /// <returns></returns>
        public virtual TContext GetDbContextInstance()
        {            
            return CreateDbContext(null);
        }
        /// <summary>
        /// Async Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public virtual async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            EntityEntry<TEntity> result;

            using (var context = GetDbContextInstance())
            {

                using (var transactions = await context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    result = await context.AddAsync(entity).ConfigureAwait(false);

                    await context.SaveChangesAsync().ConfigureAwait(false);

                    transactions.Commit();
                }

            }
            return result?.Entity;
        }
         /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> AddEntitiesAsync(IEnumerable<TEntity> entities)
        {
            if (entities.Count() <= 1)
                throw new ArgumentOutOfRangeException(nameof(entities));

            throw new NotImplementedException();
        }
        /// <summary>
        /// Async Get list of entities
        /// </summary>      
        /// <returns></returns>     
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var dbContext = GetDbContextInstance())
            {
                var dbSet = dbContext.Set<TEntity>();

                if (dbSet == null)
                    return null;

                var result = await dbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);

                return result;
            }
        }
        /// <summary>
        /// Async Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>       
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            using (var dbContext = GetDbContextInstance())
            {
                return await GetEntityById(dbContext, id);             
            }
        }

        /// <summary>
        /// Async Update entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntityEntry<TEntity> result;

            using (var dbContext = GetDbContextInstance())
            using (var transactions = await dbContext.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                result = dbContext.Update<TEntity>(entity);

                await dbContext.SaveChangesAsync().ConfigureAwait(false);

                transactions.Commit();
            }

            return result?.Entity;
        }
        /// <summary>
        /// Search by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> SearchBy(Func<TEntity, bool> predicate)
        {
            using (var dbContext = GetDbContextInstance())
            {
                var dbSet = dbContext.Set<TEntity>();

                if (dbSet == null)
                    return null;

                var result = dbSet.AsNoTracking().Where(predicate);

                return result;
            }
        }
         /// <summary>
        /// Async check if the parameter satisfies any element sequence
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyEntityAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var dbContext = GetDbContextInstance())
            {
                var dbSet = dbContext.Set<TEntity>();

                if (dbSet == null)
                    return false;

                var result = await dbSet.AsNoTracking().AnyAsync(predicate);

                return result;
            }
          
        }

    }
}