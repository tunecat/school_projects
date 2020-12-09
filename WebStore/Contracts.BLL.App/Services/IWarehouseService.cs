using System;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IWarehouseService: IBaseEntityService<Warehouse>, IWarehouseRepository<Guid, Warehouse>
    {
        
    }
}