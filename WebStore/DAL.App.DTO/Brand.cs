﻿using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;

using Domain;
 using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Brand : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public virtual string Name { get; set; } = default!;

        public virtual string? Description { get; set; }

        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } 

        public ICollection<Item>? Items { get; set; }
    }

}