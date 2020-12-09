using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Warehouse : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public virtual string Name { get; set; } = default!;
        
        public string Location { get; set; } = "Location";

        // FK
        public ICollection<ItemInWarehouse>? WarehouseItems { get; set; }

        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } // Publisher
    }
}