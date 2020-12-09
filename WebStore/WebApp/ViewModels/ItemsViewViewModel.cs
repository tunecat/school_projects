using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ItemsViewViewModel
    {
        public IEnumerable<Item> Items = default!;
        //public IEnumerable<Brand> Brands = default!;
        //public IEnumerable<Category> Categories = default!;
        public MultiSelectList? CategorySelectList { get; set; } 
        public MultiSelectList? BrandSelectList { get; set; } 
        public IEnumerable<Guid>? BrandsFilter {get; set;}
        public IEnumerable<Guid>? CategoriesFilter{get; set;}

        public string? Search { get; set; } = "";

    }
}