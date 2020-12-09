using System;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepositoryCustom: ICategoryRepositoryCustom<Guid, Category>
    {
    }
    
    public interface ICategoryRepositoryCustom<in TKey, TCategory> : IBaseRepository<TKey, TCategory> 
        where TCategory : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        
    }
}