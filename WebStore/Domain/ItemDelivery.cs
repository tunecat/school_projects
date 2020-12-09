using System;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.DAL.Base;


namespace Domain
{
    public class ItemDelivery : DomainEntity
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
        
    }
}