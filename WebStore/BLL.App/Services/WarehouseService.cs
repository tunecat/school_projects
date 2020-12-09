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
    public class WarehouseService: BaseEntityService<IWarehouseRepository, IAppUnitOfWork, IWarehouseServiceMapper,DAL.App.DTO.Warehouse, BLL.App.DTO.Warehouse>, IWarehouseService
    {
        public WarehouseService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new WarehouseServiceMapper(), unitOfWork.Warehouses)
        {
        }

        public override async Task<Warehouse> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = new Warehouse()
            {
                Id = dalEntity.Id,
                UserId = dalEntity.UserId,
                Name = dalEntity.Name,
            };
            var items = new List<ItemInWarehouse>();
            foreach (var warehouseItem in dalEntity.WarehouseItems!)
            {
                if (warehouseItem != null)
                {
                    items.Add(new ItemInWarehouse
                    {
                        WarehouseId = warehouseItem!.WarehouseId,
                        Id = warehouseItem!.Id,
                        ItemId = warehouseItem!.ItemId,
                        ItemsInStock = warehouseItem!.ItemsInStock,
                        Item = new Item
                        {
                            Name = warehouseItem!.Item!.Name,
                            Id = warehouseItem!.Item!.Id,
                            Price =  warehouseItem!.Item!.Price,
                            Description =  warehouseItem!.Item!.Description,
                            Discount =  warehouseItem!.Item!.Discount,
                            BrandId =  warehouseItem!.Item!.BrandId,
                            UserId =  warehouseItem!.Item!.UserId,
                        
                        }
                    });
                }
                
            }
            result.WarehouseItems = items;
            return result;
        }
    }
}