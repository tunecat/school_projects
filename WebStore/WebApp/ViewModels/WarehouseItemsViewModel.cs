using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class WarehouseItemsViewModel
    {
        public Warehouse Warehouse { get; set; } = default!;
        public MultiSelectList? ItemsSelectList { get; set; }
        public ICollection<Guid>? Items { get; set; }
    }
} 