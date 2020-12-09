using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;
using Domain;

namespace DAL.App.DTO
{
    public class Item: IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public virtual string Name { get; set; } = default!;

        public virtual string? Description { get; set; }

        public virtual decimal Price { get; set; } = default!;
        
        public virtual decimal Discount { get; set; } = 0;

      
        public Guid UserId { get; set; } = default!; // Publisher
        public AppUser? User { get; set; }
        
        public ICollection<ItemInCart>? ItemInCarts { get; set; }
        
        public ICollection<ItemDelivery>? DeliveredItems { get; set; }
        
        public ICollection<ItemInWarehouse>? ItemInWarehouses { get; set; }

        public ICollection<ItemCategory>? ItemCategories { get; set; }

        public Guid BrandId { get; set; } = default!;
        public Brand? Brand { get; set; }

    }
}