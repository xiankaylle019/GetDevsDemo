using ClientAPI.Data.Core;

namespace ClientAPI.Data
{
    public abstract class DataRepositoryBase <TEntity> : DataRepositoryBase<TEntity, DataContext> where TEntity : class
    {
        
    }
}