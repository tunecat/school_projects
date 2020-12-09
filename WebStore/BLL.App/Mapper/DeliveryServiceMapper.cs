using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;

namespace BLL.App.Mapper
{ 
    public class DeliveryServiceMapper : BaseBLLMapper<DAL.App.DTO.Delivery, BLL.App.DTO.Delivery>, IDeliveryServiceMapper
    {
        public DeliveryServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Delivery, BLL.App.DTO.Delivery>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Delivery, DAL.App.DTO.Delivery>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemDelivery, BLL.App.DTO.ItemDelivery>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemDelivery, DAL.App.DTO.ItemDelivery>();   
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.DeliveryType, BLL.App.DTO.DeliveryType>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DeliveryType, DAL.App.DTO.DeliveryType>();  
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();  
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();  

            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }
    }
}