using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;
using com.nipetu.webstore.DAL.Base;


namespace DAL.App.DTO
{
    public class DeliveryType : IDomainEntity<Guid>
    {
        // PK - Guid
        
        // FK
        public Guid UserId { get; set; } = default!; // admin
        public AppUser? User { get; set; }

        public ICollection<Delivery>? Deliveries { get; set; }
        
        public decimal DeliveryCost { get; set; } = 0;

        // Props
        [MinLength(2)][MaxLength(64)]public string Name { get; set; } = default!;

        [Range(0, 5)]
        public int Value { get; set; } = default!;

        public Guid Id { get; set; }
    }
}