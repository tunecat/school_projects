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
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartMapper _cartMapper;

        
        /// <summary>
        /// Carts controller for admin
        /// </summary>
        /// <param name="bll"></param>
        public CartsController(IAppBLL bll)
        {
            _bll = bll;
            _cartMapper = new CartMapper();
        }

        // GET: Carts
        /// <summary>
        /// get all customer's carts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Index()
        {
            var carts = await _bll.CartService.AllAsync();
            return Ok(carts.Select(c => _cartMapper.Map(c)));
        }

        // GET: Carts/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.CartService
                .FirstOrDefaultAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(_cartMapper.Map(cart));
        }


        // GET: Carts/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.CartService.FirstOrDefaultAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            await _bll.CartService.RemoveAsync(cart);
            await _bll.SaveChangesAsync();
            return Ok();
        }
        
    }
}