using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class DeliveryType : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid
        
        // FK
        public Guid UserId { get; set; } = default!; // admin
        public AppUser? User { get; set; }

        public ICollection<Delivery>? Deliveries { get; set; }

        [Range(0, 10000)]
        public decimal DeliveryCost { get; set; } = 0;
        // Props
        [MinLength(2)][MaxLength(64)]public string Name { get; set; } = default!;

        [Range(0, 5)]
        public int Value { get; set; } = default!;
    }
}