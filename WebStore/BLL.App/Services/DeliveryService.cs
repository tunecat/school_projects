using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.DTO;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Delivery = BLL.App.DTO.Delivery;
using ItemInCart = BLL.App.DTO.ItemInCart;

namespace BLL.App.Services
{
    public class DeliveryService :
        BaseEntityService<IDeliveryRepository, IAppUnitOfWork, IDeliveryServiceMapper, DAL.App.DTO.Delivery, Delivery>, IDeliveryService
    {
        public DeliveryService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new DeliveryServiceMapper(), unitOfWork.DeliveryRepository)
        {
        }

        public Delivery CreateDelivery(Delivery entity,ICollection<ItemInCart> items, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);
            
            foreach (var item in items)
            {
                UOW.ItemDeliveryRepository.Add(new ItemDelivery 
                {
                    ItemId = item.ItemId,
                    DeliveryId = entity.Id
                });
            }
            
           
            
            return result;
        }
    }
}