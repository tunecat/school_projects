using System;
using System.ComponentModel.DataAnnotations.Schema;
using com.nipetu.webstore.Contracts.DAL.Base;

using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserWishList : IDomainEntity<Guid>
    {
        // PK - Guid
        
        // FK
        public Guid ItemId { get; set; } = default!;
        [ForeignKey(nameof(ItemId))] public Item? Item { get; set; }
        
        public Guid UserId { get; set; } = default!; // Customer
        public AppUser? User { get; set; }

        public Guid Id { get; set; }
    }
}