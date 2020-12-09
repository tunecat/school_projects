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
using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.EF;
using DAL.App.EF.Repositories;

namespace BLL.App.Services
{
    public class CartService :
        BaseEntityService<ICartRepository, IAppUnitOfWork, ICartServiceMapper, DAL.App.DTO.Cart, BLL.App.DTO.Cart>,
        ICartService
    {
        public CartService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new CartServiceMapper(), unitOfWork.CartRepository)
        {
        }

        private Cart CollectCart(Cart cart)
        {
            var totalCost = 0m;
            foreach (var cartItem in cart.CartItems)
            {
                totalCost += cartItem.TotalPriceWithDiscount;
            }

            cart.TotalPrice = totalCost;
            cart.TotalPriceWithoutVat = totalCost - totalCost * cart.Vat;
            return cart;
        }

        public async Task<Cart> UpdateStatusAsync(Cart entity, string status, Guid? userId = null)
        {
            var result = entity;
            if (entity.Status != status)
            {
                entity.Status = status;
                var dalEntity = Mapper.Map(entity);
                var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
                result = entity;
            }
            return result;
        }

        // Get users Cart - there can be multiple Paid carts and only one pending/empty cart
        public async Task<Cart> GetCartAsync(Guid? userId = null, bool noTracking = true)
        {
            if (userId == null) throw new Exception("Unauthorized, cant get Cart");
            var dalEntities = await Repository.AllAsync(userId, noTracking);

            // get users current cart
            var cart = dalEntities
                .FirstOrDefault(c => c.Status != CartStatuses.Paid.ToString());
            var result = Mapper.Map(cart);

            // in case user dont have cart create new
            if (result == null) result = CreateCart(userId.Value);
            result = CollectCart(result);

            return result;
        }

        // Get users Cart for index page
        public async Task<Cart> GetCartViewAsync(Guid? userId = null, bool noTracking = true)
        {
            var cart = await GetCartAsync(userId);

            // check is cart empty or not and set cart status
            var status = (cart.CartItems.Count != 0) ? CartStatuses.Pending.ToString() : CartStatuses.Empty.ToString();
            if (cart.Status != status)
            {
                cart.Status = status;
                await UpdateAsync(cart, userId);
                await UOW.SaveChangesAsync();
            }
            return cart;
        }

        // Creating cart
        public Cart CreateCart(Guid userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };
            var dalEntity = Mapper.Map(cart);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, cart);
            var result = Mapper.Map(trackedDALEntity);

            return result;
        }


        public async Task<Cart> PayAsync(Guid? userId = null)
        {
            if (userId == null) throw new Exception("Unauthorized, CHECK SErVIcEs");
            var cart = await GetCartAsync(userId.Value);
            return await UpdateStatusAsync(cart, CartStatuses.Paid.ToString(), userId);
        }

        public override async Task<IEnumerable<Cart>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await Repository.AllAsync(userId, noTracking);
            var result = dalEntities.Select(e => Mapper.Map(e)).ToList();
            
            // collect cart
            foreach (var cart in result)
            {
                var totalCost = 0m;
                foreach (var cartItem in cart.CartItems)
                {
                    totalCost += cartItem.TotalPriceWithDiscount;
                }

                cart.TotalPrice = totalCost;
                cart.TotalPriceWithoutVat = totalCost - totalCost * cart.Vat;
            }

            return result;
        }
    }
}