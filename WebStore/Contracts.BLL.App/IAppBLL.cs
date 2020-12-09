using System;
using Contracts.BLL.App.Services;
using com.nipetu.webstore.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IBrandService BrandService { get; }
        public IItemService ItemService { get; }
        public ICategoryService CategoryService { get;}
        public IWarehouseService WarehouseService { get;  }
        public IItemCategoryService ItemCategoryService { get;  }
        public IItemInWarehouseService ItemInWarehouseService { get;  }
        public IItemInCartService ItemInCartService { get;  }
        public ICartService CartService { get;  }
        public IUserWishListService UserWishListService { get;  }
        
        public IItemDeliveryService ItemDeliveryService { get;  }
        public IDeliveryService DeliveryService { get;  }
        public IDeliveryTypeService DeliveryTypeService { get;  }
    }
}