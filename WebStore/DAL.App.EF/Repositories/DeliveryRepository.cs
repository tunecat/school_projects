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
    public class DeliveryRepository : EFBaseRepository<AppDbContext, Domain.Delivery, Delivery, Domain.Identity.AppUser>, IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.Delivery, Delivery>())
        {
        }

        public override async Task<Delivery> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.Include(d => d.DeliveryType)
                .Include(d => d.ItemDeliveries)
                .ThenInclude(id => id.Item)
                .Include(i => i.User)
                .AsQueryable();
            if (userId != null) query = query.Where(c => c.UserId == (Guid) userId);

            var domainEntity = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        public override async Task<IEnumerable<Delivery>> AllAsync(object? userId = null, bool noTracking = true)
        {

            var query = RepoDbSet.Include(d => d.DeliveryType)
                .Include(d => d.ItemDeliveries)
                .ThenInclude(id => id.Item)
                .Include(i => i.User).AsQueryable();
            if (userId != null) query = query.Where(c => c.UserId == (Guid) userId);

            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}