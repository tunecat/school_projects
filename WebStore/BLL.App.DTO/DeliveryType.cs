using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using BLL.App.DTO.Identity;


namespace BLL.App.DTO
{
    public class DeliveryType : IDomainEntity<Guid>
    {
        
        // FK
        public Guid UserId { get; set; } = default!; // admin
        public AppUser? User { get; set; }

        public ICollection<Delivery>? Deliveries { get; set; }
        
        public decimal DeliveryCost { get; set; } = 0;
        
        [MinLength(2)][MaxLength(64)]public string Name { get; set; } = default!;

        // IF DELIVERY SIZE, WEIGH MATTERS => Need when using any Delivery Api https://www.omniva.ee/private/parcel/domestic_parcel
        [Range(0, 5)] public int Value { get; set; } = default!;

        public Guid Id { get; set; }
    }
}