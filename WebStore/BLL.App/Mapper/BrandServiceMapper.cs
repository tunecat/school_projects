using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;

namespace BLL.App.Mapper
{ 
    public class BrandServiceMapper : BaseBLLMapper<DAL.App.DTO.Brand, BLL.App.DTO.Brand>, IBrandServiceMapper
    {
        public BrandServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInCart, BLL.App.DTO.ItemInCart>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemInCart, DAL.App.DTO.ItemInCart>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Brand, BLL.App.DTO.Brand>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Brand, DAL.App.DTO.Brand>();
            
            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }


    }
}