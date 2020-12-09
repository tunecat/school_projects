using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class DeliveryViewModel
    {
        public ICollection<Item>? DeliveryItems { get; set; }
        public Delivery Delivery { get; set; } = default!;
        public SelectList? DeliveryTypesSelectList { get; set; }
    }
}