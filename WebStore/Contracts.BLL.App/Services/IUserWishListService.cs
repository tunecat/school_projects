using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IUserWishListService: IBaseEntityService<UserWishList>, IWarehouseRepository<Guid, UserWishList>
    {
        Task<IEnumerable<Item>> GetUsersWishListAsync(Guid userGuidId);
        Task RemoveUsersWishListAsync(Guid userGuidId);
        Task RemoveFromWishListAsync(Guid itemId, Guid userGuidId);
        Task AddProduct(Guid itemId, Guid userGuidId);
    }
}