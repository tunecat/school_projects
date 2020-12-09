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
    [Authorize(Roles = "Admin, Publisher")]
    public class ItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _itemMapper;
        private readonly CategoryMapper _categoryMapper;
        private readonly BrandMapper _brandMapper;

        public ItemsController(IAppBLL bll)
        {
            _bll = bll;
            _itemMapper = new ItemMapper();
            _categoryMapper = new CategoryMapper();
            _brandMapper = new BrandMapper();
        }

        /// <summary>
        /// GET: api/Items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = User.UserRole().Contains("Admin")
                ? (await _bll.ItemService.AllAsync())
                : (await _bll.ItemService.AllAsync(User.UserGuidId()));
            var result = items.Select(i => _itemMapper.Map(i));
            return Ok(result);
        }
        
        /// <summary>
        /// GET: api/Items/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getItemDTO/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Item>> GetItemDTO(Guid id)
        {
            var item = User.UserRole().Contains("Admin")
                ? (await _bll.ItemService.FirstOrDefaultAsync((Guid) id))
                : (await _bll.ItemService.FirstOrDefaultAsync((Guid) id, User.UserGuidId()));
            
            var brands = User.UserRole().Contains("Admin")
                ? (await _bll.BrandService.AllAsync())
                : (await _bll.BrandService.AllAsync(User.UserGuidId()));
            
            if (item == null)
            {
                return NotFound();
            }
            
            
            
            var dto = new ItemDTO()
            {
                Item = _itemMapper.MapToItemDisplay(item),
                CategoriesForCreation = _bll.CategoryService.GetAllSubCategoriesAsync().Result.Select(c => _categoryMapper.Map(c)),
                BrandsForCreation = brands.Select(b => _brandMapper.Map(b))
            };
            
            return Ok(dto);
            
        }

        /// <summary>
        /// GET: api/Items/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = User.UserRole().Contains("Admin")
                ? (await _bll.ItemService.FirstOrDefaultAsync((Guid) id))
                : (await _bll.ItemService.FirstOrDefaultAsync((Guid) id, User.UserGuidId()));

            if (item == null)
            {
                return NotFound();
            }
            
            var result = _itemMapper.MapToItemDisplay(item);

            return Ok(result);
            
        }

        /// <summary>
        /// PUT: api/Items/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemEditDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutItem(Guid id, ItemEdit itemEditDTO)
        {
            if (id != itemEditDTO.Id)
            {
                return BadRequest();
            }
            
            var item = User.UserRole().Contains("Admin")
                ? (await _bll.ItemService.FirstOrDefaultAsync((Guid) id))
                : (await _bll.ItemService.FirstOrDefaultAsync((Guid) id, User.UserGuidId()));

            if (item == null)
            {
                return NotFound();
            }
            
            item.Name = itemEditDTO.Name;
            item.Description = itemEditDTO.Description;
            item.Discount = itemEditDTO.Discount;
            item.Price = itemEditDTO.Price; 
            item.BrandId = itemEditDTO.BrandId; 
            await _bll.ItemService.UpdateAsync(item);

            try
            {
                await _bll.SaveChangesAsync();
                return Ok(itemEditDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return Ok();
        }

        /// <summary>
        /// POST: api/Items
        /// </summary>
        /// <param name="itemCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Item>> PostItem(ItemCreate itemCreateDTO)
        {
            itemCreateDTO.UserId = User.UserGuidId();
            var item = _itemMapper.ItemCreate(itemCreateDTO);
            _bll.ItemService.Add(item);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetItem", new {id = item.Id}, item);
        }

        /// <summary>
        /// DELETE: api/Items/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Item>> DeleteItem(Guid id)
        {
            var item = User.UserRole().Contains("Admin")
                ? (await _bll.ItemService.FirstOrDefaultAsync((Guid) id))
                : (await _bll.ItemService.FirstOrDefaultAsync((Guid) id, User.UserGuidId()));
            
            if (item == null)
            {
                return NotFound();
            }

            await _bll.ItemService.RemoveAsync(item);
            await _bll.SaveChangesAsync();

            return Ok();
        }
        
        
    }
}