using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientAPI.Data.Core {
    public interface IDataRepository {

    }
    public interface IDataRepository<TEntity> where TEntity : class {

        Task<IEnumerable<TEntity>> GetAllAsync ();
        Task<TEntity> GetByIdAsync (int id);
        Task<TEntity> AddEntityAsync (TEntity entity);
        Task<IEnumerable<TEntity>> AddEntitiesAsync (IEnumerable<TEntity> entities);
        IEnumerable<TEntity> SearchBy (Func<TEntity, bool> predicate);
        Task<bool> AnyEntityAsync (Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateEntityAsync (TEntity entity);
    }
}