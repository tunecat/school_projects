using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CategoryCreateEditViewModel
    {
        public Category Category { get; set; } = default!;

        public SelectList? CategorySelectList { get; set; }


    }
}