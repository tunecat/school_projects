using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemInWarehouseViewModel
    {
        public ItemInWarehouse ItemInWarehouse { get; set; } = default!;
        public string? ErrorMessage { get; set; }
    }
}