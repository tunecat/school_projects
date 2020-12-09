using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;

namespace BLL.App.Mapper
{
    public class CategoryServiceMapper: BaseBLLMapper<DAL.App.DTO.Category, BLL.App.DTO.Category>, ICategoryServiceMapper
    {
        public CategoryServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Category, BLL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, DAL.App.DTO.Category>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemCategory, BLL.App.DTO.ItemCategory>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemCategory, DAL.App.DTO.ItemCategory>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, BLL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, DAL.App.DTO.Item>();

            Mapper = new AutoMapper.Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}