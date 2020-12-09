using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class Warehouse : WarehouseEdit
    {
        public int WarehouseItemsCount { get; set; }
    }

    public class WarehouseDisplay : WarehouseEdit
    {
        public ICollection<Item>? WarehouseItems {get; set;}
    }

    public class WarehouseCreate
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
    }

    public class WarehouseEdit : WarehouseCreate
    {
        public Guid Id { get; set; }
    }
}