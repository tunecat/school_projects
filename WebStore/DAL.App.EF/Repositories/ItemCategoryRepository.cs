using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemCategoryRepository : EFBaseRepository<AppDbContext,Domain.ItemCategory,DAL.App.DTO.ItemCategory, Domain.Identity.AppUser>, IItemCategoryRepository
    {
        public ItemCategoryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.ItemCategory, ItemCategory>())
        {
        }

        public override async Task<IEnumerable<ItemCategory>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(ic => ic.Category)
                .Include(ic => ic.Item);
            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}