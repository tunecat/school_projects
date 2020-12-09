using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface IDeliveryService : IBaseEntityService<Delivery>, IDeliveryRepository<Guid, Delivery>
    {
        Delivery CreateDelivery(Delivery entity, ICollection<ItemInCart> items, object? userId = null);
    }
}