using System;

namespace PublicApi.DTO.v2
{
    public class ItemInCart : ItemInCartCreate // everything need to display
    {
        public Guid Id { get; set; } = default!;
        
        public Item? Item { get; set; }

        public decimal TotalPriceWithDiscount { get; set; } = default!;
        
        public decimal ItemPriceWithDiscount { get; set; } = default!;
        
        public int Amount { get; set; } = default!;
    }
    
    public class ItemInCartCreate
    {
        public Guid CartId { get; set; } = default!;
        public Guid ItemId { get; set; } = default!;
    }
    
    
    
}