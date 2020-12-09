using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO
{
    public class Item : ItemEdit
    { 
        public decimal ItemPriceWithDiscount => Price - Price * Discount;
        public int SeparatedWarehousesCount { get; set; } = default!; // how much warehouses have that item
        //public int ItemInWarehousesCount { get; set; } = default!;  // total item count in all warehouses
    }

    public class ItemDisplay : ItemEdit
    {
        //public ICollection<ItemInCart>? ItemInCarts { get; set; }
        
        //public ICollection<ItemDelivery>? DeliveredItems { get; set; }
        
        public ICollection<ItemInWarehouse>? ItemInWarehouses { get; set; }

        public ICollection<ItemCategory>? ItemCategories { get; set; }
        
        public Brand? Brand { get; set; }
    }
    
    public class ItemCreate
    {
        public Guid BrandId { get; set; } = default!;

        [MinLength(2)][MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(2)][MaxLength(255)] public string? Description { get; set; }

        [Range(0, 10000)] public decimal Price { get; set; } = default!;
        public decimal Discount { get; set; } = 0;
        

        
    }
    
    public class ItemEdit : ItemCreate
    {
        public Guid Id { get; set; }
    }
}