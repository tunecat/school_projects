using System;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface IItemInCartService : IBaseEntityService<ItemInCart>, IItemInCartRepository<Guid, ItemInCart>
    {
        Task<ItemInCart> AddProduct(Item item, Cart cart, Guid? userId = null);
        Task<ItemInCart> DecreaseQuantity(Guid cartItemId, Guid? userId = null);
        Task<ItemInCart> IncreaseQuantity(Guid cartItemId, Guid? userId = null);

        //Task RemoveAllCartItems(Guid cartId, Guid userGuidId);
    }
}