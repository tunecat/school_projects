using System;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserWishListRepository : IUserWishListRepository<Guid, UserWishList>, IBaseRepository<UserWishList>
    {
    }

    public interface IUserWishListRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
    }
}