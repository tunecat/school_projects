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
    public class ItemsViewController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BrandMapper _brandMapper;
        private readonly CategoryMapper _categoryMapper;
        private readonly ItemMapper _itemMapper;


        public ItemsViewController(IAppBLL bll)
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
        /// <param name="brandId">Nullable/</param>
        /// <returns>Products View Page for customer</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDisplayView>>> IndexView(Guid? brandId)
        {
            var items = (await _bll.ItemService.AllAsync("", null,
                (brandId == null) ? null : new List<Guid> {(Guid) brandId}));
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
            var items = (await _bll.ItemService.AllAsync(filterSearch.Search, filterSearch.CategoriesFilter,
                filterSearch.BrandsFilter));

            var itemsView = new ItemDisplayView()
            {
                Items = items.Select(i => _itemMapper.MapToItemDisplay(i)),
                Categories = _bll.CategoryService.GetAllSubCategoriesAsync().Result
                    .Select(c => _categoryMapper.Map(c)),
                Brands = _bll.BrandService.AllAsync().Result.Select(b => _brandMapper.Map(b))
            };

            return Ok(itemsView);
        }


        /// <summary>
        /// GET: api/ItemsView/5
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>Product detail page for customer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Details(Guid id)
        {
            var item = await _bll.ItemService
                .FirstOrDefaultAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            // mapping
            var result = _itemMapper.MapToItemDisplay(item);

            return Ok(result);
        }
        
        [HttpPost("AddToCart")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddToCart(ItemIdDTO dto)
        {
            var item = await _bll.ItemService.FirstOrDefaultAsync(dto.ItemId);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());

            if (item == null || cart == null)
            {
                return NotFound();
            }

            await _bll.ItemInCartService.AddProduct(item, cart, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("AddToWishList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddToWishList(ItemIdDTO dto)
        {
            var item = await _bll.ItemService.FirstOrDefaultAsync(dto.ItemId);

            if (item == null)
            {
                return NotFound();
            }

            await _bll.UserWishListService.AddProduct(dto.ItemId, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }
    }
}