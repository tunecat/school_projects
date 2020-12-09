using System.Collections.Generic;
using Domain.Identity;

namespace WebApp.ViewModels
{
    public class AdminAreaViewModel
    {
        public IEnumerable<(AppUser user, IList<string> roles )> Users { get; set; } = default!;
        
    }
}