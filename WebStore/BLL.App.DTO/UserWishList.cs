using System;
using System.ComponentModel.DataAnnotations.Schema;
using com.nipetu.webstore.Contracts.DAL.Base;

using BLL.App.DTO.Identity;

namespace BLL.App.DTO
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