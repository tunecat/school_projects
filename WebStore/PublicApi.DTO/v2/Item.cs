using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v2
{
    public class Item : ItemEdit
    { 
        public decimal ItemPriceWithDiscount => Price - Price * Discount / 100;
        public IEnumerable<Category>? Categories { get; set; } // Item Category list
        public Brand? Brand { get; set; }
    }
    
    public class ItemDTO
    {
        public Item Item { get; set; } = default!;
        public IEnumerable<Category>? CategoriesForCreation { get; set; }
        public IEnumerable<Brand>? BrandsForCreation { get; set; }
    }


    public class ItemDisplayView 
    {
        public IEnumerable<Brand>? Brands { get; set; } // for filter select list
        public IEnumerable<Category>? Categories { get; set; } // for filter select list

        public IEnumerable<Item>? Items { get; set; }
    }
    
    public class ItemCreate
    {
        public Guid BrandId { get; set; } = default!;
        public Guid? UserId { get; set; } = null;
        [MinLength(2)][MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(2)][MaxLength(255)] public string? Description { get; set; }

        [Range(0, 10000)] public decimal Price { get; set; } = default!;
        public decimal Discount { get; set; } = 0;

        public ICollection<Guid>? ItemCategories { get; set; } // for creation
        
    }
    
    public class ItemEdit : ItemCreate
    {
        public Guid Id { get; set; }
    }
}