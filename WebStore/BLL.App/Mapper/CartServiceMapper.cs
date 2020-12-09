using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;

namespace BLL.App.Mapper
{ 
    public class CartServiceMapper : BaseBLLMapper<DAL.App.DTO.Cart, BLL.App.DTO.Cart>, ICartServiceMapper
    {
        public CartServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInCart, BLL.App.DTO.ItemInCart>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemInCart, DAL.App.DTO.ItemInCart>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Cart, BLL.App.DTO.Cart>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Cart, DAL.App.DTO.Cart>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }
    }
}