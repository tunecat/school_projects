using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Item : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid
        
        // Props
        [MinLength(2)][MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(2)][MaxLength(255)] public string? Description { get; set; }

        [Range(0, 10000)] public decimal Price { get; set; } = default!;
        
        [DataType(DataType.Currency)]public decimal Discount { get; set; } = 0;

        // FK
        public Guid UserId { get; set; } = default!; // Publisher
        public AppUser? User { get; set; }
        
        public ICollection<ItemInCart>? ItemInCarts { get; set; }
        
        public ICollection<ItemDelivery>? DeliveredItems { get; set; }
        
        public ICollection<ItemInWarehouse>? ItemInWarehouses { get; set; }

        public ICollection<ItemCategory>? ItemCategories { get; set; }

        public Guid BrandId { get; set; } = default!;
        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }
        
    }
}