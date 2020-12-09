using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.Mappers;
using PublicApi.DTO.v2;

namespace WebApp.ApiControllers._1._1
{
    [ApiController]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SearchPageController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BrandMapper _brandMapper;
        private readonly CategoryMapper _categoryMapper;
        private readonly ItemMapper _itemMapper;


        public SearchPageController(IAppBLL bll)
        {
            _bll = bll;
            _brandMapper = new BrandMapper();
            _categoryMapper = new CategoryMapper();
            _itemMapper = new ItemMapper();
        }

        /// <summary>
        /// GET: api/v1.1/ItemsView |
        /// Returns All Products / Categories and Brands for filtration
        /// </summary>
        /// <param name="search">Nullable/</param>
        /// <returns>Products View Page for customer</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDisplayView>>> IndexView(string? search = "")
        {
            var items = (await _bll.ItemService.AllAsync(search));

            var itemsView = new ItemDisplayView()
            {
                Items = items.Select(i => _itemMapper.MapToItemDisplay(i)),
                Categories = _bll.CategoryService.GetAllSubCategoriesAsync().Result.Select(c => _categoryMapper.Map(c)),
                Brands = _bll.BrandService.AllAsync().Result.Select(b => _brandMapper.Map(b))
            };
            return Ok(itemsView);
        }

        
        /// <summary>
        /// POST: api/v1.1/ItemsView |
        /// Returns All Products / Categories and Brands for filtration
        /// </summary>
        /// <param name="filterSearch">Object with search string and Collections filtration by brand or/and category</param>
        /// <returns>Filtered Products View Page for customer</returns>
        [HttpPost]
        public async Task<ActionResult<ItemDisplayView>> IndexView(FilterSearch filterSearch)
        {
            var items = (await _bll.ItemService.AllAsync(filterSearch.Search, filterSearch.CategoriesFilter, filterSearch.BrandsFilter));

            var itemsView = new ItemDisplayView()
            {
                Items = items.Select(i => _itemMapper.MapToItemDisplay(i)),
                Categories = _bll.CategoryService.GetAllSubCategoriesAsync().Result.Select(c => _categoryMapper.Map(c)),
                Brands = _bll.BrandService.AllAsync().Result.Select(b => _brandMapper.Map(b))
            };
            
            return Ok(itemsView);
        }
        

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(Guid itemId)
        {
            var item = await _bll.ItemService.FirstOrDefaultAsync(itemId);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());

            if (item == null)
            {
                return NotFound();
            }
            
            await _bll.ItemInCartService.AddProduct(item, cart, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("AddToWisList")]
        public async Task<IActionResult> AddToWishList(Guid itemId)
        {
            var item = await _bll.ItemService.FirstOrDefaultAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }
            
            await _bll.UserWishListService.AddProduct(itemId, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }
    }

}