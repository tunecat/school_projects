using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemInCartRepository : IItemInCartRepository<Guid, ItemInCart>, IBaseRepository<ItemInCart>
    {
    }
    
    public interface IItemInCartRepository<in TKey, TEntity> : IBaseRepository<TKey, TEntity> 
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        Task RemoveAllCartItems(Guid cartId, Guid userGuidId);
    }
}