using System;
using BLL.App.DTO;

namespace PublicApi.DTO.Mappers
{
    public class ItemInCartMapper:IPublicDTOMapper<BLL.App.DTO.ItemInCart, v2.ItemInCart>
    {
        public ItemMapper ItemMapper { get; set; }

        public ItemInCartMapper()
        {
            ItemMapper = new ItemMapper();
        }
        
        public ItemInCart Map(v2.ItemInCart inObject)
        {
            throw new System.NotImplementedException();
        }

        public v2.ItemInCart Map(ItemInCart outObject)
        {
            var im = new v2.ItemInCart()
            {
                Id = outObject.Id,
                CartId = outObject.CartId,
                ItemId = outObject.ItemId,
                Item = ItemMapper.Map(outObject.Item!),
                ItemPriceWithDiscount = outObject.ItemPriceWithDiscount,
                TotalPriceWithDiscount = outObject.TotalPriceWithDiscount,
                Amount = outObject.ItemAmount
            };
            return im;
        }
        
    }
}