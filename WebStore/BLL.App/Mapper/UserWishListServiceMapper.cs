using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;

namespace BLL.App.Mapper
{
    public class UserWishListServiceMapper: BaseBLLMapper<DAL.App.DTO.UserWishList, BLL.App.DTO.UserWishList>, IUserWishListServiceMapper
    {
        public UserWishListServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Brand, BLL.App.DTO.Brand>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Brand, DAL.App.DTO.Brand>();
             
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.UserWishList, BLL.App.DTO.UserWishList>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.UserWishList, DAL.App.DTO.UserWishList>();

            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }

}