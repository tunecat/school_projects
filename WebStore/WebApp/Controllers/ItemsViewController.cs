using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ItemsViewController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemsViewController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Index method , creates view model for view, collects all items, brands and categories
        /// </summary>
        /// <param name="brandId">comes from BrandsView -> </param>
        /// <returns>All products</returns>
        [HttpGet]
        public async Task<IActionResult> Index(Guid? brandId = null)
        {
            var vm = new ItemsViewViewModel
            {
                CategorySelectList = new MultiSelectList(
                    await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id),
                    nameof(Category.Name)),
                BrandSelectList = new MultiSelectList(
                    await _bll.BrandService.AllAsync(), nameof(Brand.Id), nameof(Brand.Name))
            };
            vm.Items = await _bll.ItemService.AllAsync("", null,
                (brandId == null) ? null : new List<Guid> {(Guid) brandId});

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ItemsViewViewModel vm)
        {
            vm.CategorySelectList = new MultiSelectList(
                await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id),
                nameof(Category.Name));
            vm.BrandSelectList = new MultiSelectList(
                await _bll.BrandService.AllAsync(), nameof(Brand.Id), nameof(Brand.Name));
            vm.Items = await _bll.ItemService.AllAsync("", vm.CategoriesFilter, vm.BrandsFilter);

            return View(vm);
        }

        public async Task<IActionResult> AddToCart(Guid? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService.FirstOrDefaultAsync(itemId.Value);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());

            await _bll.ItemInCartService.AddProduct(item, cart, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToWishList(Guid? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            await _bll.UserWishListService.AddProduct(itemId.Value, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService
                .FirstOrDefaultAsync((Guid) id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
    }
}