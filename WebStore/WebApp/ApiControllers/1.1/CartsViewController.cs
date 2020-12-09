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
    public class CartsViewController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartMapper _cartMapper;
        private readonly ItemInCartMapper _itemInCartMapper;
        private readonly ItemMapper _itemMapper;

        /// <summary>
        /// Cart controller
        /// </summary>
        /// <param name="bll"></param>
        public CartsViewController(IAppBLL bll)
        {
            _bll = bll;
            _cartMapper = new CartMapper();
            _itemInCartMapper = new ItemInCartMapper();
            _itemMapper = new ItemMapper();
        }

        // GET: Carts
        /// <summary>
        /// Get user's cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Cart>> Index()
        {
            var cart = await _bll.CartService.GetCartViewAsync(User.UserGuidId());
            return Ok(_cartMapper.MapDisplay(cart));
        }

        /// <summary>
        /// Clear cart items
        /// </summary>
        /// <param name="dto">Cart Id</param>
        /// <returns>View</returns>
        [HttpPost("Clear")]
        public async Task<IActionResult> ClearCart(IdDTO dto)
        {
            await _bll.ItemInCartService.RemoveAllCartItems(dto.Id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Item in cart have attribute 'amount', increase item amount
        /// </summary>
        /// <param name="dto">item in cart Id</param>
        /// <returns>View</returns>
        [HttpPost("IncreaseQuantity")]
        public async Task<IActionResult> IncreaseQuantity(IdDTO dto)
        {
            if (dto == null) return NotFound();
            var item = await _bll.ItemInCartService.FirstOrDefaultAsync(dto.Id);
            if (item == null) return NotFound();
            await _bll.ItemInCartService.IncreaseQuantity(dto.Id);
            await _bll.SaveChangesAsync();
            return Ok(_itemInCartMapper.Map(item));
        }

        /// <summary>
        /// Item in cart have attribute 'amount', decrease item amount
        /// </summary>
        /// <param name="dto">item in cart Id</param>
        /// <returns>View</returns>
        [HttpPost("DecreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity(IdDTO dto)
        {
            if (dto == null) return NotFound();
            var item = await _bll.ItemInCartService.FirstOrDefaultAsync(dto.Id);
            if (item == null) return NotFound();
            await _bll.ItemInCartService.DecreaseQuantity(dto.Id);
            await _bll.SaveChangesAsync();
            return Ok(_itemInCartMapper.Map(item));
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="dto">item in cart Id</param>
        /// <returns></returns>
        [HttpPost("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(IdDTO dto)
        {
            if (dto == null) return NotFound();
            await _bll.ItemInCartService.RemoveAsync(dto.Id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return Ok();
        }
        
    }
}