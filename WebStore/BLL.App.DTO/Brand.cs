﻿using System;
 using System.Collections.Generic;
 using BLL.App.DTO.Identity;
 using com.nipetu.webstore.Contracts.DAL.Base;


 namespace BLL.App.DTO
{
    public class Brand : IDomainEntity<Guid>
    {
        public Guid Id { get; set; } = default!;

        public string? Test { get; set; }
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public Guid UserId { get; set; } = default!;
        public AppUser? User { get; set; } 

        public ICollection<Item>? Items { get; set; }
        
        //public virtual int ItemsCount { get; set; }
    }
}