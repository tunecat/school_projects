using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;

namespace BLL.App.Mapper
{
    public class WarehouseServiceMapper: BaseBLLMapper<DAL.App.DTO.Warehouse, BLL.App.DTO.Warehouse>, IWarehouseServiceMapper
    {
        public WarehouseServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Warehouse, BLL.App.DTO.Warehouse>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Warehouse, DAL.App.DTO.Warehouse>();  
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInWarehouse, BLL.App.DTO.ItemInWarehouse>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemInWarehouse, DAL.App.DTO.ItemInWarehouse>();    
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        

    }

}