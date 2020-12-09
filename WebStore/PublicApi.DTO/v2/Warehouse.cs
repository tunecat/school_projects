using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v2
{
    
    public class WarehouseDTO
    {
        public WarehouseDisplay Warehouse { get; set; } = default!;
        public IEnumerable<Item>? Items { get; set; }
    }
    
    public class WarehouseItemsDTO
    {
        public Guid Warehouse { get; set; } = default!;
        public IEnumerable<Item>? Items { get; set; }
    }
    public class Warehouse : WarehouseEdit
    {
    }

    public class WarehouseDisplay : WarehouseEdit
    {
        public IEnumerable<ItemInWarehouse>? WarehouseItems {get; set;}
    }

    public class WarehouseCreate
    {
        public Guid? UserId { get; set; } = null;
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
    }

    public class WarehouseEdit : WarehouseCreate
    {
        public Guid Id { get; set; }
    }
    
    public class ItemInWarehouse 
    {
        public int ItemsInStock { get; set; } = default;

        public Guid ItemId { get; set; } = default!;
        
        public Guid WarehouseId { get; set; } = default!;

        public Guid Id { get; set; } = default!;

        public Item? Item { get; set; }
    }  
    
    public class ItemInWarehouseDisplay : ItemInWarehouse
    {
        //public Item? Item { get; set; }
        public Warehouse? Warehouse { get; set; }

    }    
}