using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientAPI.Data.Core
{
    public interface IDataRepository
    {

    } 
    public interface IDataRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get list of entities <see cref="IEnumerable{ TEntity }"/>
        /// </summary>
        /// <remarks>
        /// This method can also override and include associated table you want to retrieve.
        /// </remarks>
        /// <returns></returns>     
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// Get entity by Id, can also override and include associated table you want to retrieve.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        Task<TEntity> GetByIdAsync(int id);
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddEntityAsync(TEntity entity);
        /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddEntitiesAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Search by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SearchBy(Func<TEntity, bool> predicate);
        /// <summary>
        /// Check if has any data
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyEntityAsync(Expression<Func<TEntity,bool>> predicate);       /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateEntityAsync(TEntity entity);
    }
}