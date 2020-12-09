using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemInWarehouseRepository : IItemInWarehouseRepository<Guid, ItemInWarehouse>, IBaseRepository<ItemInWarehouse>
    {
    

    }
    public interface IItemInWarehouseRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity> 
        where TDALEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
    }
}