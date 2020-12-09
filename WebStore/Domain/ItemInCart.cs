using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class ItemInCart : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid

        // FK
     
        [Display(Name = nameof(Cart), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Item? Item { get; set; }
        [Display(Name = nameof(Item), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public Guid ItemId { get; set; } = default!;

        // Props
        [Display(Name = nameof(ItemPrice), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public decimal ItemPrice { get; set; } = default!;

        [Display(Name = nameof(ItemAmount), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        [Range(1, 10000)] public int ItemAmount { get; set; } = default!;
        public decimal Vat { get; set; } = 0.2m;

        [Display(Name = nameof(ItemPriceWithoutVat), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public decimal ItemPriceWithoutVat { get; set; } = default!;
        
        [Display(Name = nameof(Discount), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public decimal Discount { get; set; } = default!;
        
        [Display(Name = nameof(ItemPriceWithDiscount), ResourceType = typeof(Resources.Domain.ItemInCart.ItemInCart))]
        public decimal ItemPriceWithDiscount { get; set; } = default!;
        
        public decimal ItemPriceWithoutVatWithDiscount { get; set; } = default!;

        public decimal TotalPrice { get; set; } = default!;

    }
}