using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    /// <summary>
    /// Admin area controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminAreaController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        /// <summary>
        /// dummy constructor
        /// </summary>
        public AdminAreaController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        ///  Dummy index page
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminAreaViewModel
            {
                Users = (await _userManager.Users.ToListAsync())
                    .Select(e => (User: e, Roles: _userManager.GetRolesAsync(e).Result))
            };
            return View(viewModel);
        }
        
        /// <summary>
        ///  Dummy index page
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> MakeUserAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        ///  Dummy index page
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> MakeUserPublisher(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "Publisher");

            return RedirectToAction(nameof(Index));
        }

    }
}