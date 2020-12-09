using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Warehouse : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid

        // props
        [MinLength(2)][MaxLength(64)]
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Warehouse.Warehouse))]
        public string Name { get; set; } = default!;

        public string Location { get; set; } = "Location";
        // FK
        public ICollection<ItemInWarehouse>? WarehouseItems { get; set; }
        
        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } // Publisher

    }
}