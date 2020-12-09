using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using com.nipetu.webstore.Contracts.DAL.Base;

using BLL.App.DTO.Identity;


namespace BLL.App.DTO
{
    public class Delivery : IDomainEntity<Guid>
    {
        // PK - Guid
        
        // FK
        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; }
        
        public ICollection<ItemDelivery> ItemDeliveries { get; set; } = new List<ItemDelivery>();

        public Guid DeliveryTypeId { get; set; } = default!;
        public DeliveryType? DeliveryType { get; set; }

        // delivery cost
        [Range(0, 10000)]
        public decimal Cost { get; set; } = default!;

        // delivery cost + cart cost
        public decimal TotalCost { get; set; } = 0;

        [MinLength(2)][MaxLength(100)]
        public string Destination { get; set; } = default!;

        public DateTime? DeliveredAt { get; set; }

        
        public string Status { get; set; } = "Accepted"; // Accepted, Delivered

        public Guid Id { get; set; }
    }
}