using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.EF.Repositories;
using com.nipetu.webstore.DAL.Base.EF
;
using Domain;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IBrandRepository Brands => 
            GetRepository<IBrandRepository>
            (() => new BrandRepository(UOWDbContext));

        public IItemRepository Items => GetRepository<IItemRepository>
            (() => new ItemRepository(UOWDbContext));
        
        public ICategoryRepository Categories => GetRepository<ICategoryRepository>
            (() => new CategoryRepository(UOWDbContext));
        
        public IItemCategoryRepository ItemCategories => GetRepository<IItemCategoryRepository>
            (() => new ItemCategoryRepository(UOWDbContext));
        
        public IWarehouseRepository Warehouses => GetRepository<IWarehouseRepository>
            (() => new WarehouseRepository(UOWDbContext));
        
        public IItemInWarehouseRepository ItemsInWarehouse => GetRepository<IItemInWarehouseRepository>
            (() => new ItemInWarehouseRepository(UOWDbContext));

        public IItemInCartRepository ItemInCartRepository => GetRepository<IItemInCartRepository>
            (() => new ItemInCartRepository(UOWDbContext));
        
        
        public ICartRepository CartRepository => GetRepository<ICartRepository>
            (() => new CartRepository(UOWDbContext));
        
        public IDeliveryRepository DeliveryRepository => GetRepository<IDeliveryRepository>
            (() => new DeliveryRepository(UOWDbContext));
        
        public IDeliveryTypeRepository DeliveryTypeRepository => GetRepository<IDeliveryTypeRepository>
            (() => new DeliveryTypeRepository(UOWDbContext));
        
        public IItemDeliveryRepository ItemDeliveryRepository => GetRepository<IItemDeliveryRepository>
            (() => new ItemDeliveryRepository(UOWDbContext));
        
        public IPaymentsRepository PaymentsRepository => GetRepository<IPaymentsRepository>
            (() => new PaymentsRepository(UOWDbContext));
        
        public IUserWishListRepository UserWishListRepository => GetRepository<IUserWishListRepository>
            (() => new UserWishListRepository(UOWDbContext));
        


        /*
          public IBrandRepository Brands => new BrandRepository(UOWDbContext);  // transit
          public ICategoryRepository Categories => new CategoryRepository(UOWDbContext);
          public IItemCategoryRepository ItemCategories => new ItemCategoryRepository(UOWDbContext);
          public IWarehouseRepository Warehouses => new WarehouseRepository(UOWDbContext);
          public IItemInWarehouseRepository ItemsInWarehouse => new ItemInWarehouseRepository(UOWDbContext);
         */

        
    }
}