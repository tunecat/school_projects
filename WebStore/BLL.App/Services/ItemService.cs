using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLL.App.Mapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Item = BLL.App.DTO.Item;

namespace BLL.App.Services
{
    public class ItemService :
        BaseEntityService<IItemRepository, IAppUnitOfWork, IItemServiceMapper, DAL.App.DTO.Item, BLL.App.DTO.Item>,
        IItemService

    {
        public ItemService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new ItemServiceMapper(), unitOfWork.Items)
        {
        }

        public override Item Add(Item entity)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);

            if (entity.Categories == null) return result;
            foreach (var categoryId in entity!.Categories)
            {
                var itemCategory = new ItemCategory
                {
                    ItemId = entity.Id,
                    CategoryId = categoryId
                };
                UOW.ItemCategories.Add(itemCategory);
            }


            return result;
        }

        public async Task<IEnumerable<Item>> AllAsync(string? searchString = "", IEnumerable<Guid>? categoryFilter = null,
            IEnumerable<Guid>? brandFilter = null)
        {
            var dalEntities = await Repository.AllAsync(searchString, categoryFilter, brandFilter);
            var result = dalEntities.Select(e => Mapper.Map(e));

            return result;
        }
        
    }
}