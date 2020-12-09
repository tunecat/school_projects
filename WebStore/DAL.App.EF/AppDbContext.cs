using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;

using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private IUserNameProvider _userNameProvider;
        
        private readonly Dictionary<IDomainEntity<Guid>, IDomainEntity<Guid>> _entityTracker =
            new Dictionary<IDomainEntity<Guid>, IDomainEntity<Guid>>();

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemCategory> ItemCategories { get; set; } = default!;
        
        public DbSet<Brand> Brands { get; set; } = default!;
        
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<ItemInCart> ItemInCarts { get; set; } = default!;
        
        public DbSet<Delivery> Deliveries { get; set; } = default!;
        public DbSet<DeliveryType> DeliveryTypes { get; set; } = default!;
        public DbSet<ItemDelivery> ItemDeliveries { get; set; } = default!;
        
        public DbSet<Payments> Payments { get; set; } = default!;

        // public DbSet<User> Users { get; set; }

        public DbSet<UserWishList> UserWishLists { get; set; } = default!;

        public DbSet<Warehouse> Warehouses { get; set; } = default!;
        public DbSet<ItemInWarehouse> ItemInWarehouses { get; set; } = default!;


        
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public void AddToEntityTracker(IDomainEntity<Guid> internalEntity, IDomainEntity<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
        
        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}