using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemRepository : IItemRepository<Guid, Item>, IBaseRepository<Item>
    {
    }
    
    public interface IItemRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity>
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> AllAsync(string? searchString = "", IEnumerable<Guid>? categoryFilter = null,
            IEnumerable<Guid>? brandFilter = null);
    }
    
    
    
}