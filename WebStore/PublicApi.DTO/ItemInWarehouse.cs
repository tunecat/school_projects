using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class ItemInWarehouse : ItemInWarehouseEdit
    {
    }
    
    // for display only
    public class ItemInWarehouseDisplay : ItemInWarehouseEdit
    {
        public Item? Item { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
    // from client to server
    public class ItemInWarehouseCreate
    {
        [Range(0, 100000)] 
        public int ItemsInStock { get; set; } = default;

        public Guid ItemId { get; set; } = default!;

        public Guid WarehouseId { get; set; } = default!;
    }
    // from client to server
    public class ItemInWarehouseEdit : ItemInWarehouseCreate
    {
        public Guid Id { get; set; }
    }
    

}