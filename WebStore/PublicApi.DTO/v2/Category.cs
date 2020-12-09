using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v2
{
    // from display only
    public class CategoryDisplay : CategoryEdit
    {
        public ICollection<Category>? ChildCategories { get; set; } = default!;
        public ICollection<Item>? CategoryItems { get; set; } = default!;
        public Category? ParentCategory { get; set; }
    }
    
    // from client to server
    public class Category: CategoryEdit
    {
        //public int ChildCategoriesCount { get; set; } = default!;
        //public int CategoryItemsCount { get; set; } = default!;
        public IEnumerable<Category>? ChildCategories { get; set; }
    }
  
    // from client to server
    public class CategoryCreate
    {
        [MaxLength(36)][MinLength(2)] public string Name { get; set; } = default!;
        public Guid? ParentCategoryId { get; set; }
    }
    
    // from client to server
    public class CategoryEdit : CategoryCreate
    {
        public Guid Id { get; set; }
    }
}