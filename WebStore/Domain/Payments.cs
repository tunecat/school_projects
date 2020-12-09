using System;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.Contracts.DAL.Base;

using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Payments : DomainEntity, IDomainEntityUser<AppUser>
    {
        // PK - Guid
        
        // FK
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        // Props
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PayedAt { get; set; }

        
        [MinLength(2)][MaxLength(255)]public string Info { get; set; } = default!;

        public decimal Sum { get; set; } = default!;

  
    }
}