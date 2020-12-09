using System;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class CategoryTranslateViewModel
    {
        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();
        public string? en { get; set; }
        public string? et { get; set; }
        public string? ru { get; set; }
        public Guid CategoryId { get; set; } = default!;
    }
}