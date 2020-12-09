using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.DAL.Base.EF
.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DeliveryTypeRepository : EFBaseRepository<AppDbContext,Domain.DeliveryType,DeliveryType, Domain.Identity.AppUser>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.DeliveryType, DeliveryType>())
        {
        }
    }
}