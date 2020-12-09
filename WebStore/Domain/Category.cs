using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using com.nipetu.webstore.DAL.Base;

using Domain.Identity;

namespace Domain
{
    public class Category : DomainEntity
    {
        // PK - Guid
        // Props
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Category.Category))]
        [MaxLength(36)] [MinLength(2)] public string Name { get; set; } = default!;
        
        // Recursive FK
        public Category? ParentCategory { get; set; }
        [Display(Name = nameof(ParentCategory), ResourceType = typeof(Resources.Domain.Category.Category))]
        public Guid? ParentCategoryId { get; set; }

        public ICollection<Category>? ChildCategories { get; set; }

        public ICollection<ItemCategory>? CategoryItems { get; set; }
        
        // Props -> Dictionary<UiCulture, Translation>
        public string? Translation { get; set; }
        
    }
}