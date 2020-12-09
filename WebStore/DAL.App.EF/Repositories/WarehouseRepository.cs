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
    public class WarehouseRepository : EFBaseRepository<AppDbContext,Domain.Warehouse,DAL.App.DTO.Warehouse, Domain.Identity.AppUser>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext dbContext)
            : base(dbContext, new DALMapper<Domain.Warehouse, Warehouse>())
        {
        }

        public override async Task<IEnumerable<Warehouse>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = (userId != null) ? RepoDbSet.Where(c => c.UserId == (Guid) userId) : RepoDbSet;
            //query.Include(w => w.WarehouseItems);
            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
            
        }

        public override async Task<Warehouse> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.Include(w => w.WarehouseItems)
                .ThenInclude(ic => ic.Item)
               .AsQueryable();
            if (userId != null) query = query.Where(c => c.UserId == (Guid) userId);
            var domainEntity = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        
    }
}