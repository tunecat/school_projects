using System;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser
    {
        public Guid Id { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;

    }
}