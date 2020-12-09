using System;
using System.Linq;

namespace PublicApi.DTO.Mappers
{
    public class WarehouseMapper : IPublicDTOMapper<BLL.App.DTO.Warehouse, v2.Warehouse>
    {
        private BrandMapper BrandMapper { get; set; }
        private ItemMapper ItemMapper { get; set; }
        private CategoryMapper CategoryMapper { get; set; }
        
        public WarehouseMapper()
        {
            CategoryMapper = new CategoryMapper();
            BrandMapper = new BrandMapper();
            ItemMapper = new ItemMapper();
        }
        
        // public api dto to bll dto
        public BLL.App.DTO.Warehouse Map(v2.Warehouse inObject)
        {
            return new BLL.App.DTO.Warehouse()
            {
                Id = inObject.Id,
                UserId = inObject.UserId.GetValueOrDefault(),
                Name = inObject.Name,
            };
        }

        //  bll dto to public api dto 
        public v2.Warehouse Map(BLL.App.DTO.Warehouse outObject)
        {
            return new v2.Warehouse()
            {
                Id = outObject.Id,
                Name = outObject.Name,
                UserId = outObject.UserId
            };
        }
        
        // bll dto to itemsView dto
        public v2.WarehouseDisplay MapToWarehouseDisplay(BLL.App.DTO.Warehouse outObject)
        {
            return new v2.WarehouseDisplay()
            {
                Id = outObject.Id,
                Name = outObject.Name,
                UserId = outObject.UserId,
                WarehouseItems = outObject.WarehouseItems.Select(wi => MapToPublicDTOItemInWarehouse(wi))
            };
        }
        
        // from createDto to bll dto
        public BLL.App.DTO.Warehouse WarehouseCreate(v2.WarehouseCreate warehouseCreateDTO)
        {
            var warehouse = new BLL.App.DTO.Warehouse()
            {
                Id = Guid.NewGuid(),
                UserId = warehouseCreateDTO.UserId.GetValueOrDefault(),
                Name = warehouseCreateDTO.Name,
            };
            return warehouse;
        }

        public v2.ItemInWarehouse MapToPublicDTOItemInWarehouse(BLL.App.DTO.ItemInWarehouse itemInWarehouse)
        {
            return new v2.ItemInWarehouse()
            {
                Id = itemInWarehouse.Id,
                ItemId = itemInWarehouse.ItemId,
                ItemsInStock = itemInWarehouse.ItemsInStock,
                WarehouseId = itemInWarehouse.WarehouseId,
                Item = ItemMapper.Map(itemInWarehouse.Item!)
            };
        }
        
        public BLL.App.DTO.ItemInWarehouse MapToBllDTOItemInWarehouse(v2.ItemInWarehouse itemInWarehouse)
        {
            return new BLL.App.DTO.ItemInWarehouse()
            {
                Id = itemInWarehouse.Id,
                ItemId = itemInWarehouse.ItemId,
                ItemsInStock = itemInWarehouse.ItemsInStock,
                WarehouseId = itemInWarehouse.WarehouseId,
                
            };
        }
        
        public v2.ItemInWarehouseDisplay MapItemInWarehouseDisplay(BLL.App.DTO.ItemInWarehouse itemInWarehouse)
        {
            return new v2.ItemInWarehouseDisplay()
            {
                Id = itemInWarehouse.Id,
                Item = ItemMapper.Map(itemInWarehouse.Item!),
                ItemId = itemInWarehouse.ItemId,
                ItemsInStock = itemInWarehouse.ItemsInStock,
                WarehouseId = itemInWarehouse.WarehouseId,
                Warehouse = Map(itemInWarehouse.Warehouse!),
                
            };
        }
    }
}