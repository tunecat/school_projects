using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicApi.DTO.Mappers
{
    public class CategoryMapper : IPublicDTOMapper<BLL.App.DTO.Category, v2.Category>
    {
        // public api dto to bll dto
        public BLL.App.DTO.Category Map(v2.Category inObject)
        {
            return new BLL.App.DTO.Category()
            {
                Id = inObject.Id,
                ParentCategoryId = inObject.ParentCategoryId,
                Name = inObject.Name,
            };
        }

        // bll dto to public api dto 
        public v2.Category Map(BLL.App.DTO.Category outObject)
        {
            return new v2.Category()
            {
                Id = outObject.Id,
                ParentCategoryId = outObject.ParentCategoryId,
                Name = outObject.Name,
                ChildCategories = outObject.ChildCategories.Select(c => Map(c))
            };
        }
        
        // from createDto to bll dto
        public BLL.App.DTO.Category CategoryCreate(v2.CategoryCreate categoryCreateDTO)
        {
            var category = new BLL.App.DTO.Category()
            {
                Id = Guid.NewGuid(),
                Name = categoryCreateDTO.Name,
                ParentCategoryId = categoryCreateDTO.ParentCategoryId,
            };
            return category;
        }
        // from editDto to bll dto
        public BLL.App.DTO.Category CategoryEdit(v2.CategoryEdit categoryCreateDTO)
        {
            var category = new BLL.App.DTO.Category()
            {
                Id = categoryCreateDTO.Id,
                Name = categoryCreateDTO.Name,
                ParentCategoryId = categoryCreateDTO.ParentCategoryId,
            };
            return category;
        }
    }
}