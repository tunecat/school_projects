using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v2;
using PublicApi.DTO.Mappers;
using ItemInWarehouse = PublicApi.DTO.v2.ItemInWarehouse;

namespace WebApp.ApiControllers._1._1
{
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemInWarehousesController : ControllerBase
    {
        private readonly IAppBLL _bll;        
        private readonly WarehouseMapper _warehouseMapper;


        public ItemInWarehousesController(IAppBLL bll)
        {
            _bll = bll;
            _warehouseMapper = new WarehouseMapper();
        }
        
        
        // GET: api/ItemInWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInWarehouse>> GetItemInWarehouse(Guid id)
        {
            var itemInWarehouse = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id);
                

            if (itemInWarehouse == null)
            {
                return NotFound();
            }

            return Ok(_warehouseMapper.MapItemInWarehouseDisplay(itemInWarehouse));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">item in warehouse id</param>
        /// <param name="amount">change to amount</param>
        /// <param name="submitButton">'-' or '+'</param>
        /// <returns></returns>
        [HttpGet("ChangeAmount")]
        public async Task<IActionResult> ChangeAmount(Guid? id, int amount, string submitButton)
        {
            if (id == null) return NotFound();
            var item = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id.Value);

            switch (submitButton)
            {
                case "plus":
                    item.ItemsInStock += amount;
                    break;
                case "minus":
                    item.ItemsInStock -= amount;
                    if (item.ItemsInStock < 0) return BadRequest("Not enough products in stock"); //return RedirectToAction("Details", new{id = id, errorMessage = "Not enough products in stock"});
                    break;
                case "Empty":
                    item.ItemsInStock = 0;
                    break;
            }
            
            await _bll.ItemInWarehouseService.UpdateAsync(item);
            await _bll.SaveChangesAsync();
            return Ok(_warehouseMapper.MapToPublicDTOItemInWarehouse(item));
        }
        
        // DELETE: api/ItemInWarehouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemInWarehouse>> DeleteItemInWarehouse(Guid id)
        {
            var itemInWarehouse = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id);
            if (itemInWarehouse == null)
            {
                return NotFound();
            }

            await _bll.ItemInWarehouseService.RemoveAsync(itemInWarehouse);
            await _bll.SaveChangesAsync();
            return Ok();
        }
        
        
        
     
    }
}