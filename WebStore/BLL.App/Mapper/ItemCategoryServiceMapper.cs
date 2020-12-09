using AutoMapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;

namespace BLL.App.Mapper
{
    public class ItemCategoryServiceMapper: BaseBLLMapper<DAL.App.DTO.ItemCategory, BLL.App.DTO.ItemCategory>, IItemCategoryServiceMapper
    {
        public ItemCategoryServiceMapper()
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