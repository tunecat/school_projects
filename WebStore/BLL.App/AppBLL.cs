using System;
using System.Threading.Tasks;
using BLL.App.Services;
using com.nipetu.webstore.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using com.nipetu.webstore.Contracts.BLL.Base;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {

        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IBrandService BrandService =>
            GetService<IBrandService>(() => new BrandService(UnitOfWork));

        public IItemService ItemService =>
            GetService<IItemService>(() => new ItemService(UnitOfWork));

        public ICategoryService CategoryService =>
            GetService<ICategoryService>(() => new CategoryService(UnitOfWork));

        public IWarehouseService WarehouseService =>
            GetService<IWarehouseService>(() => new WarehouseService(UnitOfWork));

        public IItemCategoryService ItemCategoryService =>
            GetService<IItemCategoryService>(() => new ItemCategoryService(UnitOfWork));

        public IItemInWarehouseService ItemInWarehouseService =>
            GetService<IItemInWarehouseService>(() => new ItemInWarehouseService(UnitOfWork));

        public IItemInCartService ItemInCartService =>
            GetService<IItemInCartService>(() => new ItemInCartService(UnitOfWork));



        public ICartService CartService =>
            GetService<ICartService>(() => new CartService(UnitOfWork));

        public IUserWishListService UserWishListService =>
            GetService<IUserWishListService>(() => new UserWishListService(UnitOfWork));

        public IItemDeliveryService ItemDeliveryService =>
            GetService<IItemDeliveryService>(() => new ItemDeliveryService(UnitOfWork));

        public IDeliveryService DeliveryService =>
            GetService<IDeliveryService>(() => new DeliveryService(UnitOfWork));

        public IDeliveryTypeService DeliveryTypeService =>
            GetService<IDeliveryTypeService>(() => new DeliveryTypeService(UnitOfWork));

    }
}