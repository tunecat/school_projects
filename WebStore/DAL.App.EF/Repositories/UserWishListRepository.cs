using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using DAL.App.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserWishListRepository : EFBaseRepository<AppDbContext, Domain.UserWishList,UserWishList, Domain.Identity.AppUser>, IUserWishListRepository
    {
        public UserWishListRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.UserWishList, UserWishList>())
        {
        }

        public override async Task<IEnumerable<UserWishList>> AllAsync(object? userId = null, bool noTracking = true)
        {
            if(userId == null) throw new Exception("Not Authorized");
            var domainEntities = await RepoDbSet
                .Include(uwl => uwl.Item)
                .ThenInclude(i => i!.Brand)
                .Where(uwl => uwl.UserId == (Guid) userId).AsNoTracking().ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e)).ToList();
            return result;
        }
    }
}