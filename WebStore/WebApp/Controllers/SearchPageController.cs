using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class SearchPageController : Controller
    {
        private readonly IAppBLL _bll;

        public SearchPageController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET
        public async Task<IActionResult> Index(string? search = "")
        {
            search ??= "";
            var vm = new ItemsViewViewModel
            {
                Search = search,
                CategorySelectList = new MultiSelectList(
                    await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id),
                    nameof(Category.Name)),
                BrandSelectList = new MultiSelectList(
                    await _bll.BrandService.AllAsync(), nameof(Brand.Id), nameof(Brand.Name))
            };
            vm.Items = await _bll.ItemService.AllAsync(search);

            return View(vm);        
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ItemsViewViewModel vm)
        {
            if (vm.Search == null) vm.Search = "";
            vm.CategorySelectList = new MultiSelectList(
                await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id),
                nameof(Category.Name));
            vm.BrandSelectList = new MultiSelectList(
                await _bll.BrandService.AllAsync(), nameof(Brand.Id), nameof(Brand.Name));
            vm.Items = await _bll.ItemService.AllAsync(vm.Search, vm.CategoriesFilter, vm.BrandsFilter);
            
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

    }
}