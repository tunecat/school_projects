using System;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.DAL.Base;


namespace Domain
{
    public class ItemInWarehouse : DomainEntity
    {
        // PK - Guid

        [Range(0, 100000)] 
        public int ItemsInStock { get; set; } = default;

        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid WarehouseId { get; set; } = default!;
        public Warehouse? Warehouse { get; set; }
    }
}