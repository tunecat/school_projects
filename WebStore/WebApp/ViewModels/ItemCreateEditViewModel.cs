using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemCreateEditViewModel
    {
        public Item Item { get; set; } = default!;
        public MultiSelectList? CategorySelectList { get; set; } 
        public SelectList? BrandSelectList { get; set; }

        public IEnumerable<Guid>? Categories { get; set; }
        
    }
}