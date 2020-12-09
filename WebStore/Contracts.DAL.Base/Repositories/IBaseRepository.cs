using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
        where TEntity : class, IDomainEntity<Guid>, new() 
    {
    }

    public interface IBaseRepository<in TKey, TEntity>
        where TEntity : class, IDomainEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        // crud
        Task<IEnumerable<TEntity>> AllAsync(object? userId = null, bool noTracking = true);
        
        Task<TEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true);
        TEntity Add(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TKey id, object? userId = null);
        Task<bool> ExistsAsync(TKey id, object? userId = null);

    }
}