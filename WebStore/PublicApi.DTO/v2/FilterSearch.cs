using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v2
{
    public class FilterSearch
    {
        public IEnumerable<Guid>? BrandsFilter {get; set;}
        public IEnumerable<Guid>? CategoriesFilter {get; set;}

        public string? Search { get; set; } = "";
    }
}