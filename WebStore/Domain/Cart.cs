using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Cart : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid
        
        // FK
        public Guid UserId { get; set; } = default!;  // Customer
        public AppUser? User { get; set; }
        
        
        public ICollection<ItemInCart> CartItems { get; set; } = new List<ItemInCart>();

        // Props
        [MinLength(4)][MaxLength(7)]
        [Display(Name = nameof(Status), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        public string Status { get; set; } = default!; // Pending, Paid, NotPaid
       
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Display(Name = nameof(PayedAt), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        public DateTime? PayedAt { get; set; }

        [Display(Name = nameof(TotalPrice), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        [Range(0, 100000)]public decimal TotalPrice { get; set; } = default!;

        
        [Display(Name = nameof(TotalPriceWithoutVat), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        [Range(0, 100000)]public decimal TotalPriceWithoutVat { get; set; } = 0m;

        [Display(Name = nameof(Vat), ResourceType = typeof(Resources.Domain.Cart.Cart))]
        public decimal Vat { get; set; } = 0.2m;
    }
}