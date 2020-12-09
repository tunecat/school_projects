using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Newtonsoft.Json;

namespace BLL.App.Services
{
    public class CategoryService :
        BaseEntityService<ICategoryRepository, IAppUnitOfWork, ICategoryServiceMapper, DAL.App.DTO.Category,
            BLL.App.DTO.Category>,
        ICategoryService
    {
        public CategoryService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new CategoryServiceMapper(), unitOfWork.Categories)
        {
        }

        public override Category Add(Category entity)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);

            return result;
        }

        public async Task<IEnumerable<Category>> GetAllSubCategoriesAsync()
        {
            var dalEntities = await Repository.AllAsync();
            var result = dalEntities
                .Where(c => c.ParentCategoryId != null)
                .Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<Category>> GetAllParentCategoriesAsync()
        {
            var dalEntities = (await Repository.AllAsync());
            var result = dalEntities
                .Where(c => c.ParentCategoryId == null)
                .Select(e => Mapper.Map(e));
            return result;
        }
    }
}