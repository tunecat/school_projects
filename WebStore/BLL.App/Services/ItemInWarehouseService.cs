using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLL.App.Mapper;


namespace BLL.App.Services
{
    public class ItemInWarehouseService:
        BaseEntityService<IItemInWarehouseRepository, IAppUnitOfWork, IItemInWarehouseServiceMapper, DAL.App.DTO.ItemInWarehouse, BLL.App.DTO.ItemInWarehouse>, IItemInWarehouseService
    {
        public ItemInWarehouseService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new Mapper.ItemInWarehouseServiceMapper(), unitOfWork.ItemsInWarehouse)
        {
        }

        public override Task<ItemInWarehouse> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            return base.FirstOrDefaultAsync(id, null, true);
        }
    }
}