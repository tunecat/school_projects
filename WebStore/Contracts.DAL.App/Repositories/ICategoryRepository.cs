using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository : ICategoryRepository<Guid, Category> ,IBaseRepository<Category>
    {
        
    }
    public interface ICategoryRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
    }
}