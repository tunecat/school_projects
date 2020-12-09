using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class UserWishListService: BaseEntityService<IUserWishListRepository, IAppUnitOfWork, IUserWishListServiceMapper,DAL.App.DTO.UserWishList, BLL.App.DTO.UserWishList>, IUserWishListService
    {
        public UserWishListService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new UserWishListServiceMapper(), unitOfWork.UserWishListRepository)
        {
        }
        
#pragma warning disable 8613
        public async Task<IEnumerable<Item?>> GetUsersWishListAsync(Guid userGuidId)
        {
            var dalEntities = await Repository.AllAsync(userGuidId);
            var result = dalEntities
                .Select(e => Mapper.Map(e))
                .Select(e => e.Item)
                .Where(e => e != null).ToList();
            return result;
        }

        public async Task RemoveUsersWishListAsync(Guid userGuidId)
        {
            var dalEntities = await Repository.AllAsync(userGuidId);
            var userWishListItems = dalEntities
                .Where(uwl => uwl.UserId == userGuidId)
                .Select(e => Mapper.Map(e));

            foreach (var item in userWishListItems)
            {
                var resultDALEntity = await Repository.RemoveAsync(item.Id, userGuidId);
                Mapper.Map(resultDALEntity);
            }
            
        }

        // Remove wish list element by item id
        public async Task RemoveFromWishListAsync(Guid itemId, Guid userGuidId)
        {
            var dalEntities = await Repository.AllAsync(userGuidId);
            var userWishListItem = dalEntities
                .Where(uwl => uwl.UserId == userGuidId && uwl.ItemId == itemId)
                .Select(e => Mapper.Map(e)).First();
            var resultDALEntity = await Repository.RemoveAsync(userWishListItem.Id, userGuidId);
            Mapper.Map(resultDALEntity);
        }

        public async Task AddProduct(Guid itemId, Guid userGuidId)
        {
            var wishListItems = await GetUsersWishListAsync(userGuidId);
            // if item already in wish list - ignore request
            foreach (var item in wishListItems)
            {
                if(item!.Id == itemId) return;
            }
            
            var wishListItem = new UserWishList
            {
                ItemId = itemId,
                UserId = userGuidId
            };
            base.Add(wishListItem);
        }
    }
}