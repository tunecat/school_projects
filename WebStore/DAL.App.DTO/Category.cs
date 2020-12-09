using System;
using System.Collections.Generic;
using BLL.App.DTO;
using com.nipetu.webstore.Contracts.DAL.Base;

using Domain;

namespace DAL.App.DTO
{
    public class Category : IDomainEntity<Guid>
    {
        public virtual string Name { get; set; } = default!;

        // Recursive FK
        public Category? ParentCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public IEnumerable<Category>? ChildCategories { get; set; }

        public ICollection<ItemCategory>? CategoryItems { get; set; }

        public Guid Id { get; set; } = default!;
        

        // Props -> Dictionary<UiCulture, Translation>
        public string? Translation { get; set; }
    }
}