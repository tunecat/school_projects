using System;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ItemInCart : IDomainEntity<Guid>
    {
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Item? Item { get; set; }
        public Guid ItemId { get; set; } = default!;

        // Props
        public decimal ItemPrice { get; set; } = default!;

        public int ItemAmount { get; set; } = default!;
        public decimal Vat { get; set; } = 0.2m;

        public decimal ItemPriceWithoutVat { get; set; } = default!;
        
        public decimal Discount { get; set; } = default!;
        
        public decimal ItemPriceWithDiscount { get; set; } = default!;
        
        public decimal ItemPriceWithoutVatWithDiscount { get; set; } = default!;

        public decimal TotalPrice { get; set; } = default!;

        public Guid Id { get; set; }
    }
}