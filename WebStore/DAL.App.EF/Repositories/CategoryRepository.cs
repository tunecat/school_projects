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
using Newtonsoft.Json;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository :
        EFBaseRepository<AppDbContext, Domain.Category, DAL.App.DTO.Category, Domain.Identity.AppUser>,
        ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.Category, Category>())
        {
        }
        
       

        public override async Task<IEnumerable<Category>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(c => c.ChildCategories)
                .Include(c => c.ParentCategory);
            var domainEntities = await query.AsNoTracking().ToListAsync();
            
            // mapping with translations
            var result = domainEntities.Select(e =>
            {
                var name = e.Name;
                if (!string.IsNullOrEmpty(e.Translation) && e.Translation != "{}")
                {
                    name =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(e.Translation)[
                            System.Threading.Thread.CurrentThread.CurrentUICulture.Name];
                }

                var cat = new DAL.App.DTO.Category()
                {
                    Name = name,
                    Translation = e.Translation,
                    Id = e.Id,
                    ParentCategoryId = e.ParentCategoryId,
                    ChildCategories = e.ChildCategories.Select(e =>
                    {
                        var name = e.Name;
                        if (!string.IsNullOrEmpty(e.Translation) && e.Translation != "{}")
                        {
                            name =
                                JsonConvert.DeserializeObject<Dictionary<string, string>>(e.Translation)[
                                    System.Threading.Thread.CurrentThread.CurrentUICulture.Name];
                        }
                        var cat = new DAL.App.DTO.Category()
                        {
                            Name = name,
                            Translation = e.Translation,
                            Id = e.Id,
                            ParentCategoryId = e.ParentCategoryId,
                            ParentCategory = null,
                            ChildCategories = null,
                        };
                        return cat;
                    })
                };
                return cat;
            });
            return result;
        }

        public override async Task<Category> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query= RepoDbSet
                .Include(c => c.ParentCategory)
                .Include(i => i.CategoryItems);
            var domainEntity = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

    }
}