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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserWishListController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _itemMapper;

        public UserWishListController(IAppBLL bll)
        {
            _bll = bll;
            _itemMapper = new ItemMapper();
        }
        
        // GET: api/Items
        /// <summary>
        /// Get user's wish list items
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _bll.UserWishListService.GetUsersWishListAsync(User.UserGuidId());
            
            var result = items.Select(i => _itemMapper.MapToItemDisplay(i));
            return Ok(result);
        }

        // GET: api/Items/5
        /// <summary>
        /// Get information about item
        /// </summary>
        /// <param name="id">item Id</param>
        /// <returns>Item View Details page</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = (await _bll.ItemService.FirstOrDefaultAsync((Guid) id));
             
            if (item == null)
            {
                return NotFound();
            }
            
            var result = _itemMapper.MapToItemDisplay(item);
            
            return Ok(result);
            
        }

        /// <summary>
        /// Remove item from wish list
        /// </summary>
        /// <param name="dto">item Id</param>
        /// <returns></returns>
        [HttpPost("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(ItemIdDTO dto)
        {
            if (dto.ItemId == null)
            {
                return NotFound();
            }

            await _bll.UserWishListService.RemoveFromWishListAsync(dto.ItemId, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Add product to user's cart
        /// </summary>
        /// <param name="dto">Item Id</param>
        /// <returns>View</returns>
        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart(ItemIdDTO dto)
        {
            if (dto.ItemId == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService.FirstOrDefaultAsync(dto.ItemId);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());

            await RemoveProduct(dto);
            await _bll.ItemInCartService.AddProduct(item, cart, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }
        


        /// <summary>
        ///  Clear wish list
        /// </summary>
        /// <returns>View</returns>
        [HttpPost("Clear")]
        public async Task<IActionResult> Clear()
        {
            await _bll.UserWishListService.RemoveUsersWishListAsync(User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }
    }
}