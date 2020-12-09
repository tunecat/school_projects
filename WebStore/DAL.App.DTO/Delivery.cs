using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;
using com.nipetu.webstore.DAL.Base;



namespace DAL.App.DTO
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

        // Props
        [Range(0, 10000)]
        public decimal Cost { get; set; } = default!;

        [MinLength(2)][MaxLength(100)]
        public string Destination { get; set; } = default!;

        public DateTime? DeliveredAt { get; set; }

        public string Status { get; set; } = "Accepted"; // Accepted, Delivered

        public Guid Id { get; set; }
    }
}