using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemDeliveryRepository : EFBaseRepository<AppDbContext,Domain.ItemDelivery,ItemDelivery, Domain.Identity.AppUser>, IItemDeliveryRepository
    {
        public ItemDeliveryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.ItemDelivery, ItemDelivery>())
        {
        }
    }
}