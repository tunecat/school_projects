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
    public class ItemInWarehouseRepository : EFBaseRepository<AppDbContext, Domain.ItemInWarehouse, DAL.App.DTO.ItemInWarehouse, Domain.Identity.AppUser>, IItemInWarehouseRepository
    {
        public ItemInWarehouseRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.ItemInWarehouse, ItemInWarehouse>())
        {
        }

        public override async Task<ItemInWarehouse> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.Include(w => w.Item)
                .Include(w => w.Warehouse)
                .AsQueryable();
            var domainEntity = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}