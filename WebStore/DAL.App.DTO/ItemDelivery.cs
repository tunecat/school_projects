using System;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace DAL.App.DTO
{
    public class ItemDelivery : IDomainEntity<Guid>
    {
        // PK - Guid
        
        // FK
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid DeliveryId { get; set; } = default!;
        public Delivery? Delivery { get; set; }

        // Props
        [Range(0, 5)] 
        public int ItemDeliveryValue { get; set; } = default!;

        public Guid Id { get; set; }
    }
}