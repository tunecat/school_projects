using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;

namespace PublicApi.DTO.v2
{
    public class Cart : CartEdit
    {
        public IEnumerable<ItemInCart> CartItems { get; set; } = new List<ItemInCart>();
        
        public string Status { get; set; } = CartStatuses.Empty.ToString(); // Pending, Paid, NotPaid

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PayedAt { get; set; }

        public decimal TotalPrice { get; set; } = 0m;

        public decimal TotalPriceWithoutVat { get; set; } = 0m;

        public decimal Vat { get; set; } = 0.2m;
    }

    public class CartCreate
    {
        public Guid UserId { get; set; } = default!;
    }

    public class CartEdit : CartCreate
    {
        public Guid Id { get; set; }
    }
}