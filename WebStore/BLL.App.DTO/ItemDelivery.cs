using System;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace BLL.App.DTO
{
    public class ItemDelivery : IDomainEntity<Guid>
    {
        // PK - Guid
        
        // FK
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid DeliveryId { get; set; } = default!;
        public Delivery? Delivery { get; set; }
        
        // IF DELIVERY SIZE, WEIGH MATTERS => Need when using any Delivery Api https://www.omniva.ee/private/parcel/domestic_parcel
        [Range(0, 5)] public int ItemDeliveryValue { get; set; } = 1;

        public Guid Id { get; set; }
    }
}