using System.Collections.Generic;
using BLL.App.DTO;

namespace WebApp.ViewModels
{
    public class UserWishListViewModel
    {
        public IEnumerable<Item?> Items { get; set; } = new List<Item?>();
        public decimal TotalCost { get; set; } = 0;
    }
}