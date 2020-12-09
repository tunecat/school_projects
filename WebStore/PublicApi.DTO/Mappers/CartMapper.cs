using System;
using System.Collections.Generic;
using System.Linq;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public class CartMapper : IPublicDTOMapper<BLL.App.DTO.Cart, v2.Cart>
    {
        private ItemInCartMapper ItemInCartMapper { get; set; }
        
        public CartMapper()
        {
            ItemInCartMapper = new ItemInCartMapper();
        }
        
        public Cart Map(v2.Cart inObject)
        {
            return new Cart()
            {
                Id = inObject.Id,
                UserId = inObject.UserId,
                Status = inObject.Status,
                TotalPrice = inObject.TotalPrice,
                Vat = inObject.Vat,
                TotalPriceWithoutVat = inObject.TotalPriceWithoutVat,
                CreatedAt = inObject.CreatedAt,
                PayedAt = inObject.PayedAt,
            };
        }

        public v2.Cart Map(Cart outObject)
        {
            return new v2.Cart()
            {
                Id = outObject.Id,
                UserId = outObject.UserId,
                Status = outObject.Status,
                TotalPrice = outObject.TotalPrice,
                Vat = outObject.Vat,
                TotalPriceWithoutVat = outObject.TotalPriceWithoutVat,
                CreatedAt = outObject.CreatedAt,
                PayedAt = outObject.PayedAt,
                //CartItems = new List<v2.ItemInCart>(), //inObject.CartItems,
            };
        }
        
        public v2.Cart MapDisplay(Cart outObject)
        {
            return new v2.Cart()
            {
                Id = outObject.Id,
                UserId = outObject.UserId,
                Status = outObject.Status,
                TotalPrice = outObject.TotalPrice,
                Vat = outObject.Vat,
                TotalPriceWithoutVat = outObject.TotalPriceWithoutVat,
                CreatedAt = outObject.CreatedAt,
                PayedAt = outObject.PayedAt,
                CartItems = outObject.CartItems.Select(ci => ItemInCartMapper.Map(ci)),
            };
        }
        
        
        public v2.Cart CartCreate(Cart outObject)
        {
            return new v2.Cart()
            {
                Id = Guid.NewGuid(),
                Status = outObject.Status,
                TotalPrice = outObject.TotalPrice,
                Vat = outObject.Vat,
                TotalPriceWithoutVat = outObject.TotalPriceWithoutVat,
                CreatedAt = outObject.CreatedAt,
            };
        }
    }
}