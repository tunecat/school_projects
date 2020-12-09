using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using DAL.App.EF;
using DAL.App.EF.Repositories;

namespace BLL.App.Services
{
    public class ItemDeliveryService :
        BaseEntityService<IItemDeliveryRepository, IAppUnitOfWork, IItemDeliveryServiceMapper, DAL.App.DTO.ItemDelivery, ItemDelivery>, IItemDeliveryService
    {
        public ItemDeliveryService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new ItemDeliveryServiceMapper(), unitOfWork.ItemDeliveryRepository)
        {
        }
    }
}