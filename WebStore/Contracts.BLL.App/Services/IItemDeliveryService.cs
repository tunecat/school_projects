using System;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface IItemDeliveryService : IBaseEntityService<ItemDelivery>, IItemDeliveryRepository<Guid, ItemDelivery>
    {
    }
}