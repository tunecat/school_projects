using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : EFBaseRepository<AppDbContext,Domain.Item,DAL.App.DTO.Item, Domain.Identity.AppUser>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.Item, Item>())
        {
        }

        public override async Task<IEnumerable<Item>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = (userId != null) ? RepoDbSet.Where(c => c.UserId == (Guid) userId) : RepoDbSet;

            query = query.Include(i => i.Brand)
                .Include(i => i.ItemCategories)
                .ThenInclude(ic => ic.Category);
            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Item> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        { 
      
            var query = RepoDbSet.Include(i => i.Brand)
                .Include(i => i.ItemCategories)
                .ThenInclude(ic => ic.Category).AsQueryable();

            if (userId != null) query = query.Where(c => c.UserId == (Guid) userId);
            var domainEntity = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;       
        }

        public async Task<IEnumerable<Item>> AllAsync(string? searchString = "",
            IEnumerable<Guid>? categoryFilter = null, IEnumerable<Guid>? brandFilter = null)
        {
            var query = RepoDbSet
                    // Search result
                .Where(i => i.Name.ToLower().Contains(searchString!.ToLower()))
                .Include(i => i.Brand)
                .Include(i => i.ItemCategories)
                .ThenInclude(ic => ic.Category);
            
            IEnumerable<Domain.Item> domainEntities = (await query.AsNoTracking().ToListAsync());
            
            // filter everything
            if (brandFilter != null || categoryFilter != null)
            {
                domainEntities = domainEntities.Where(i =>
                {
                    // check filter by brands id exist
                    if (brandFilter != null)
                    {
                        // if item is suitable by brand check for categories
                        if (brandFilter.Contains(i.BrandId))
                        {
                        
                            if (categoryFilter != null)
                            {
                                if (filterCategory(i, categoryFilter)) return true;
                            }
                            else
                            {
                                return true;
                            }

                            return true;
                        }
                    }
                    else
                    {
                        if (categoryFilter != null)
                        {
                            if (filterCategory(i, categoryFilter)) return true;
                        }
                    }

                    return false;
                });
            }
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        // check item have categories
        private bool filterCategory(Domain.Item item, IEnumerable<Guid> categoryFilter)
        {
            foreach (var filterCategoryId in categoryFilter)
            {
                foreach (var itemCategoryId in item.ItemCategories.Select(i => i.CategoryId))
                {
                    if (itemCategoryId == filterCategoryId) return true;
                }
            }

            return false;
        }
    }
}