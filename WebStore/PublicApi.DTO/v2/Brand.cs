using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v2
{
    public class Brand : BrandEdit
    {
    }

    public class BrandCreate
    {
        public Guid? UserId { get; set; } = null;
        
        [MinLength(1)] [MaxLength(64)]
        public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(512)] 
        public string? Description { get; set; }
    }

    public class BrandEdit : BrandCreate
    {
        public Guid Id { get; set; }
    }
}