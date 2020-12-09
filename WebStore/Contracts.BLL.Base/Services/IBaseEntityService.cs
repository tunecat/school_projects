using System;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;


namespace Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainEntity<Guid>, new()
    {
    }

    public interface IBaseEntityService<in TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntity<TKey>, new()
    {
        
    }

}
        
 