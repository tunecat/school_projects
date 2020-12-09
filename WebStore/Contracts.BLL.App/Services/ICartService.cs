using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface ICartService : IBaseEntityService<Cart>, ICartRepository<Guid, Cart>
    {
        Task<Cart> GetCartAsync(Guid? userId = null, bool noTracking = true);
        Cart CreateCart(Guid userId);
        Task<Cart> PayAsync(Guid? userId = null);

        Task<Cart> UpdateStatusAsync(Cart entity, string status, Guid? userId = null);
        Task<Cart> GetCartViewAsync(Guid? userId = null, bool noTracking = true);
    }
}