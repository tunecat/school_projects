using System.Collections.Generic;

namespace PublicApi.DTO.v2
{
    public class Culture
    {
        public string EntityName { get; set; } = default!;
        public IEnumerable<CultureDTO> CultureDTO { get; set; } = default!;
    }
    public class CultureDTO
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}