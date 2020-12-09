using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;

using BLL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class Cart : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public Guid UserId { get; set; } = default!; // Customer
        public AppUser? User { get; set; }


        public ICollection<ItemInCart> CartItems { get; set; } = new List<ItemInCart>();

        // Props
        public string Status { get; set; } = CartStatuses.Empty.ToString(); // Pending, Paid, NotPaid

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PayedAt { get; set; }

        public decimal TotalPrice { get; set; } = 0m;

        public decimal TotalPriceWithoutVat { get; set; } = 0m;

        public decimal Vat { get; set; } = 0.2m;
    }
}