using AutoMapper;
using AutoMapper.Configuration;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;

namespace BLL.App.Mapper
{
    public class ItemServiceMapper: BaseBLLMapper<DAL.App.DTO.Item, BLL.App.DTO.Item>, IItemServiceMapper
    {

        public ItemServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInCart, BLL.App.DTO.ItemInCart>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemInCart, DAL.App.DTO.ItemInCart>();

            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Brand, BLL.App.DTO.Brand>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Brand, DAL.App.DTO.Brand>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemCategory, BLL.App.DTO.ItemCategory>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemCategory, DAL.App.DTO.ItemCategory>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Category, BLL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, DAL.App.DTO.Category>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }
       
    }
}
