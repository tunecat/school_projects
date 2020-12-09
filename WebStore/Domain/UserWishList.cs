using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class UserWishList : DomainEntity
    {
        // PK - Guid
        
        // FK
        public Guid ItemId { get; set; } = default!;
        [ForeignKey(nameof(ItemId))] public Item? Item { get; set; }
        
        public Guid UserId { get; set; } = default!; // Customer
        [ForeignKey(nameof(UserId))] 
        public AppUser? User { get; set; }
    }
}