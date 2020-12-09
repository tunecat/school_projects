using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentsRepository : EFBaseRepository<AppDbContext,Payments,Payments, Domain.Identity.AppUser>, IPaymentsRepository
    {
        public PaymentsRepository(AppDbContext dbContext) : base(dbContext, new IdentityMapper<Payments, Payments>())
        {
        }
    }
}