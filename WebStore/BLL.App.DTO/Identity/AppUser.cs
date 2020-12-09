using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }

    public class AppUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Delivery>? Deliveries { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
    }
}