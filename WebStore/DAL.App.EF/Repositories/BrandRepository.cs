using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using com.nipetu.webstore.DAL.Base.EF.Repositories;
using com.nipetu.webstore.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BrandRepository :
        EFBaseRepository<AppDbContext, Domain.Brand, DAL.App.DTO.Brand, Domain.Identity.AppUser>, IBrandRepository
    {
        public BrandRepository(AppDbContext dbContext)
            : base(dbContext, new DALMapper<Domain.Brand, DAL.App.DTO.Brand>())
        {
        }

        public override async Task<Brand> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query.Include(b => b.Items)
                .ThenInclude(i => i.ItemCategories);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}