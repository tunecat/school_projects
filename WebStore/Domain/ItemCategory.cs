using System;
using com.nipetu.webstore.DAL.Base;


namespace Domain
{
    public class ItemCategory : DomainEntity
    {
        // PK - Guid

        // FK
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}