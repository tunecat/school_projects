using System;
using com.nipetu.webstore.Contracts.DAL.Base;



namespace BLL.App.DTO
{
    public class ItemInWarehouse : IDomainEntity<Guid>
    {
        public virtual int ItemsInStock { get; set; } = default;

        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid WarehouseId { get; set; } = default!;
        public Warehouse? Warehouse { get; set; }

        public Guid Id { get; set; } = default!;
    }
}