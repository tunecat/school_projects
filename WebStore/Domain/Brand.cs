﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Brand : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid
        
        // props
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Brand))]
        [MinLength(1)] [MaxLength(64)]
        public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(508)] 
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Brand))]
        public string? Description { get; set; }
        
        // FK
        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } // Publisher
        
        public ICollection<Item>? Items { get; set; }
        
    }
}