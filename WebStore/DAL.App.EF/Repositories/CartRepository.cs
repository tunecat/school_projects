using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;

namespace DAL.App.EF.Repositories
{
    public class CartRepository : EFBaseRepository<AppDbContext, Domain.Cart, Cart, Domain.Identity.AppUser>,
        ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.Cart, Cart>())
        {
        }


        /// <summary>
        /// if userId is null -> returns all carts from db, else -> only user's carts
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noTracking"></param>
        /// <returns>List of all carts</returns>
        public override async Task<IEnumerable<Cart>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = (userId != null) ? RepoDbSet.Where(c => c.UserId == (Guid) userId) : RepoDbSet;
            
            query = query
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Item);
            var domainEntities = await query.AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}