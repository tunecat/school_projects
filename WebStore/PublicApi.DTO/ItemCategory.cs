using System;

namespace PublicApi.DTO
{
    
    // for display only
    public class ItemCategory : ItemCategoryEdit
    {
    }
    
    // for display only
    public class ItemCategoryDetail : ItemCategoryEdit
    {
        public Item Item { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
    
    public class ItemCategoryCreate
    {
        public Guid CategoryId { get; set; } = default!;
        public Guid ItemId { get; set; } = default!;
    }
    
    public class ItemCategoryEdit : ItemCategoryCreate
    {
        public Guid Id { get; set; }

    }
    
}