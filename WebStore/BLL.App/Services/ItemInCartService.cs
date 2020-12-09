using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLL.App.Mapper;
using DAL.App.EF.Repositories;


namespace BLL.App.Services
{
    public class ItemInCartService :
        BaseEntityService<IItemInCartRepository, IAppUnitOfWork, IItemInCartServiceMapper, DAL.App.DTO.ItemInCart,
            BLL.App.DTO.ItemInCart>, IItemInCartService
    {
        public ItemInCartService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new Mapper.ItemInCartServiceMapper(), unitOfWork.ItemInCartRepository)
        {
        }

        public async Task<ItemInCart> AddProduct(Item item, Cart cart, Guid? userId = null)
        {
            if (userId == null) throw new Exception("Unauthorized, cant add Product to Cart");
            
            foreach (var itemInCart in cart.CartItems)
            {
                if (itemInCart.ItemId == item.Id)
                {
                     return await IncreaseQuantity(itemInCart.Id, userId); // if product already in cart just increase amount
                }
            }
            
            var cartItem = new ItemInCart
            {
                UserId = userId.Value,
                ItemId = item.Id,
                CartId = cart.Id,
                ItemPrice = item.Price,
                Discount = item.Discount,
                ItemPriceWithDiscount = item.PriceWithDiscount,
                ItemPriceWithoutVat = item.Price - item.Price * 0.2m,
                ItemPriceWithoutVatWithDiscount =
                    item.Price - item.Price * 0.2m - (item.Price - item.Price * 0.2m) * item.Discount / 100 ,
                ItemAmount = 1,
            };
            cartItem.TotalPrice = cartItem.ItemAmount * cartItem.ItemPriceWithDiscount;
            var result = base.Add(cartItem);

            return result;
        }

        public async Task<ItemInCart> DecreaseQuantity(Guid cartItemId, Guid? userId = null)
        {
            var cartItem = await base.FirstOrDefaultAsync(cartItemId, userId);
            if (cartItem.ItemAmount == 1) return await base.RemoveAsync(cartItemId);
            cartItem.ItemAmount--;
            cartItem.TotalPrice = cartItem.ItemAmount * cartItem.ItemPriceWithDiscount;
            var dalEntity = Mapper.Map(cartItem);
            var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public async Task<ItemInCart> IncreaseQuantity(Guid cartItemId, Guid? userId = null)
        {
            var cartItem = await base.FirstOrDefaultAsync(cartItemId);
            cartItem.ItemAmount++;
            cartItem.TotalPrice = cartItem.ItemAmount * cartItem.ItemPriceWithDiscount;
            return await base.UpdateAsync(cartItem);

        }

        public async Task RemoveAllCartItems(Guid cartId, Guid userGuidId)
        {
            await Repository.RemoveAllCartItems(cartId, userGuidId);
        }
    }
}