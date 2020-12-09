using System;
using com.nipetu.webstore.Contracts.DAL.Base;



namespace BLL.App.DTO
{
    public class ItemCategory : IDomainEntity<Guid>
    {
        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }

        public Guid Id { get; set; } = default!;
    }
}