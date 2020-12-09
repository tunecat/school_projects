using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Cart : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public Guid UserId { get; set; } = default!; // Customer
        public AppUser? User { get; set; }


        public ICollection<ItemInCart> CartItems { get; set; } = new List<ItemInCart>();

        // Props
        public string Status { get; set; } = default!; // Pending, Paid, NotPaid

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PayedAt { get; set; }

        public decimal TotalPrice { get; set; } = default!;

        public decimal TotalPriceWithoutVat { get; set; } = 0m;

        public decimal Vat { get; set; } = 0.2m;
    }
}