using System;
using com.nipetu.webstore.Contracts.DAL.Base;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepositoryCustom : ICartRepositoryCustom<Guid, Cart>
    {
    }
    
    public interface ICartRepositoryCustom<in TKey, TEntity> : IBaseRepository<TKey, TEntity> 
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        
    }
}