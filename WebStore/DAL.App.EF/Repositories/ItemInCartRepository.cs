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
    public class ItemInCartRepository : EFBaseRepository<AppDbContext, Domain.ItemInCart, ItemInCart, Domain.Identity.AppUser>, IItemInCartRepository
    {
        public ItemInCartRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.ItemInCart, ItemInCart>())
        {
        }

        public override async Task<IEnumerable<ItemInCart>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.Include(i => i.Item);

            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<ItemInCart> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var domainEntity = await RepoDbSet.Include(i => i.Item).AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
            
        }

        public async Task RemoveAllCartItems(Guid cartId, Guid userGuidId)
        {
            var domainEntities = await base.AllAsync();
                
            var cartItems = domainEntities.Where(ci => ci.CartId == cartId).Select(e => Mapper.Map(e)).ToList();
            foreach (var cartItem in cartItems)
            {
                await base.RemoveAsync(cartItem.Id, userGuidId);
            }
        }
    }
}