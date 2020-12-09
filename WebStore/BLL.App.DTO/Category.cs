using System;
using System.Collections.Generic;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace BLL.App.DTO
{
    public class Category : IDomainBaseEntity, IDomainEntity<Guid>
    {
        public virtual string Name { get; set; } = default!;

        // Recursive FK
        public Category? ParentCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public ICollection<Category>? ChildCategories { get; set; }

        public ICollection<ItemCategory>? CategoryItems { get; set; }

        public Guid Id { get; set; } = default!;
        
        // Props -> Dictionary<UiCulture, Translation>
        public string? Translation { get; set; }
        public Dictionary<string, string>? TranslationObjects { get; set; }
        
    }
}