using System;
using System.Linq;

namespace PublicApi.DTO.Mappers
{
    public class ItemMapper : IPublicDTOMapper<BLL.App.DTO.Item, v2.Item>
    {
        private BrandMapper BrandMapper { get; set; }
        private CategoryMapper CategoryMapper { get; set; }
        
        public ItemMapper()
        {
            CategoryMapper = new CategoryMapper();
            BrandMapper = new BrandMapper();
        }
        
        // public api dto to bll dto
        public BLL.App.DTO.Item Map(v2.Item inObject)
        {
            return new BLL.App.DTO.Item()
            {
                Id = inObject.Id,
                Description = inObject.Description,
                UserId = inObject.UserId.GetValueOrDefault(),
                Price = inObject.Price,
                BrandId = inObject.BrandId,
                Discount = inObject.Discount,
                Name = inObject.Name,
                Categories = inObject.ItemCategories
            };
        }

        //  bll dto to public api dto 
        public v2.Item Map(BLL.App.DTO.Item outObject)
        {
            return new v2.Item()
            {
                Id = outObject.Id,
                Description = outObject.Description,
                UserId = outObject.UserId,
                Price = outObject.Price,
                BrandId = outObject.BrandId,
                Discount = outObject.Discount,
                Name = outObject.Name,
                Brand = outObject.Brand == null ? null : BrandMapper.Map(outObject.Brand),
                Categories = null
            };
        }
        
        // bll dto to itemsView dto
        public v2.Item MapToItemDisplay(BLL.App.DTO.Item outObject)
        {
            return new v2.Item()
            {
                Id = outObject.Id,
                Description = outObject.Description,
                UserId = outObject.UserId,
                Price = outObject.Price,
                BrandId = outObject.BrandId,
                Discount = outObject.Discount,
                Name = outObject.Name,
                Brand = outObject.Brand == null ? null : BrandMapper.Map(outObject.Brand),
                Categories = outObject.ItemCategories.Select(ic => CategoryMapper.Map(ic.Category!))
            };
        }
        
        // from createDto to bll dto
        public BLL.App.DTO.Item ItemCreate(v2.ItemCreate itemCreateDTO)
        {
            var item = new BLL.App.DTO.Item()
            {
                Id = Guid.NewGuid(),
                UserId = itemCreateDTO.UserId.GetValueOrDefault(),
                Name = itemCreateDTO.Name,
                Description = itemCreateDTO.Description,
                BrandId = itemCreateDTO.BrandId,
                Price = itemCreateDTO.Price,
                Discount = itemCreateDTO.Discount,
                Categories = itemCreateDTO.ItemCategories
            };
            return item;
        }
    }
}