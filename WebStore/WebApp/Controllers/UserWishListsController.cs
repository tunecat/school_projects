using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserWishListsController : Controller
    {
        private readonly IAppBLL _bll;

        public UserWishListsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get user's wish list items
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            var vm = new UserWishListViewModel();
            vm.Items = await _bll.UserWishListService.GetUsersWishListAsync(User.UserGuidId());
            foreach (var item in vm.Items)
            {
                vm.TotalCost += item!.ItemPriceWithDiscount;
            }

            return View(vm);
        }

        /// <summary>
        /// Remove item from wish list
        /// </summary>
        /// <param name="itemId">item Id</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveProduct(Guid? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            await _bll.UserWishListService.RemoveFromWishListAsync(itemId.Value, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Add product to user's cart
        /// </summary>
        /// <param name="itemId">Item Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> AddProductToCart(Guid? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService.FirstOrDefaultAsync(itemId.Value);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());

            await RemoveProduct(itemId);
            await _bll.ItemInCartService.AddProduct(item, cart, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get information about item
        /// </summary>
        /// <param name="id">item Id</param>
        /// <returns>Item Details page</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService.FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return Redirect($"../../itemsView/Details/{id}");
        }


        /// <summary>
        ///  Clear wish list
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Clear()
        {
            await _bll.UserWishListService.RemoveUsersWishListAsync(User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}