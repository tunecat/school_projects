using AutoMapper;
using com.nipetu.webstore.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class, new()
        where TLeftObject : class, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<Domain.Brand, DAL.App.DTO.Brand>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Brand, Domain.Brand>();
            
            MapperConfigurationExpression.CreateMap<Domain.Item, DAL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, Domain.Item>();
            
            MapperConfigurationExpression.CreateMap<Domain.Cart, DAL.App.DTO.Cart>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Cart, Domain.Cart>();
            
            MapperConfigurationExpression.CreateMap<Domain.Warehouse, DAL.App.DTO.Warehouse>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Warehouse, Domain.Warehouse>();
            
            MapperConfigurationExpression.CreateMap<Domain.Category, DAL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Category, Domain.Category>(); 
            
            MapperConfigurationExpression.CreateMap<Domain.ItemInCart, DAL.App.DTO.ItemInCart>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInCart, Domain.ItemInCart>(); 
            
            MapperConfigurationExpression.CreateMap<Domain.ItemCategory, DAL.App.DTO.ItemCategory>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemCategory, Domain.ItemCategory>(); 
            
            MapperConfigurationExpression.CreateMap<Domain.ItemInWarehouse, DAL.App.DTO.ItemInWarehouse>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInWarehouse, Domain.ItemInWarehouse>();
            
            MapperConfigurationExpression.CreateMap<Domain.Delivery, DAL.App.DTO.Delivery>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Delivery, Domain.Delivery>(); 
            
            MapperConfigurationExpression.CreateMap<Domain.DeliveryType, DAL.App.DTO.DeliveryType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.DeliveryType, Domain.DeliveryType>(); 

            MapperConfigurationExpression.CreateMap<Domain.ItemDelivery, DAL.App.DTO.ItemDelivery>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemDelivery, Domain.ItemDelivery>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}