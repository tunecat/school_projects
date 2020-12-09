using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace BLL.App.DTO
{
    public class Warehouse : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public virtual string Name { get; set; } = default!;

        // FK
        public ICollection<ItemInWarehouse>? WarehouseItems { get; set; }
        
        public string Location { get; set; } = "Location";

        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } // Publisher
    }
}