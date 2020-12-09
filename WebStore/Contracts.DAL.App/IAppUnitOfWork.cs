using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IBrandRepository Brands { get; }
        
        IItemRepository Items{ get; }
        
        ICategoryRepository Categories { get; }
        IItemCategoryRepository ItemCategories { get; }
        
        IWarehouseRepository Warehouses { get; }
        IItemInWarehouseRepository ItemsInWarehouse { get; }
        
        IItemInCartRepository ItemInCartRepository { get; }
        ICartRepository CartRepository { get; }
        
        IDeliveryRepository DeliveryRepository{ get; }
        IDeliveryTypeRepository DeliveryTypeRepository{ get; }
        IItemDeliveryRepository ItemDeliveryRepository{ get; }

        IPaymentsRepository PaymentsRepository{ get; }
        
        IUserWishListRepository UserWishListRepository{ get; }     
        
    }
}