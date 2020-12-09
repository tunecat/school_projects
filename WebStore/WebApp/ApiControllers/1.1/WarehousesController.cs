using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
    [ApiVersion( "1.1" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Publisher")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WarehousesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WarehouseMapper _warehouseMapper;
        private readonly ItemMapper _itemMapper;
        

        public WarehousesController(IAppBLL bll)
        {
            _bll = bll;
            _warehouseMapper = new WarehouseMapper();
            _itemMapper = new ItemMapper();
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouse()
        {
            var warehouses = User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.AllAsync()
                : await _bll.WarehouseService.AllAsync(User.UserGuidId());


            return Ok(warehouses.Select(w => _warehouseMapper.Map(w)));
        }


        /// <summary>
        /// GET: api/Warehouses/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouse(Guid id)
        {
            var warehouse = User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.FirstOrDefaultAsync(id)
                : await _bll.WarehouseService.FirstOrDefaultAsync(id, User.UserGuidId());

            if (warehouse == null)
            {
                return NotFound();
            }
            var items = User.UserRole().Contains("Admin")
                ? await _bll.ItemService.AllAsync()
                : await _bll.ItemService.AllAsync(User.UserGuidId());
            
            // get list of items which not in warehouse
            items = items.Where(i =>
                {
                    var boole = true;
                    foreach (var warehouseItem in warehouse.WarehouseItems!)
                    {
                        if (i.Id == warehouseItem.ItemId)
                        {
                            boole = false;
                        }
                    }
                    return boole;
                }
            );
            var dto = new WarehouseDTO()
            {
                Warehouse = _warehouseMapper.MapToWarehouseDisplay(warehouse),
                Items = items.Select(i => _itemMapper.Map(i))
            };

            return Ok(dto);
        }
        

        /// <summary>
        /// PUT: api/Warehouses/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouseEditDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, WarehouseEdit warehouseEditDTO)
        {
            if (id != warehouseEditDTO.Id)
            {
                return BadRequest();
            }

            var warehouse = User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.FirstOrDefaultAsync(id)
                : await _bll.WarehouseService.FirstOrDefaultAsync(id, User.UserGuidId());

            warehouse.Name = warehouseEditDTO.Name;
            await _bll.WarehouseService.UpdateAsync(warehouse);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return Ok(_warehouseMapper.Map(warehouse));
        }
        
        /// <summary>
        /// POST: api/Warehouses
        /// </summary>
        /// <param name="warehouseCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseCreate warehouseCreateDTO)
        {
            warehouseCreateDTO.UserId = User.UserGuidId();
            var warehouse = new BLL.App.DTO.Warehouse
            {
                UserId = warehouseCreateDTO.UserId.GetValueOrDefault(),
                Name = warehouseCreateDTO.Name,
                Id = Guid.NewGuid()
            };
            _bll.WarehouseService.Add(warehouse);
            await _bll.SaveChangesAsync();

            return Ok();
        }
        
        /// <summary>
        /// DELETE: api/Warehouses/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Warehouse>> DeleteWarehouse(Guid id)
        {
            var warehouse = User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.FirstOrDefaultAsync(id)
                : await _bll.WarehouseService.FirstOrDefaultAsync(id, User.UserGuidId());

            if (warehouse == null)
            {
                return NotFound();
            }

            await _bll.WarehouseService.RemoveAsync(warehouse);
            await _bll.SaveChangesAsync();

            return Ok(warehouse);
        }
        


        /// <summary>
        /// POST: api/Warehouses/AddItemsToWarehouse
        /// </summary>
        /// <param name="warehouseItemsDTO"></param>
        /// <returns></returns>
        [HttpPost("AddItemsToWarehouse")]
        public async Task<IActionResult> AddItemsToWarehouse(WarehouseItemsDTO warehouseItemsDTO)
        {
            foreach (var item in warehouseItemsDTO.Items!)
            {
                var newItem = _bll.ItemInWarehouseService.Add(_warehouseMapper.MapToBllDTOItemInWarehouse(new ItemInWarehouse
                {
                    Id = Guid.NewGuid(),
                    ItemId = item.Id,
                    WarehouseId = warehouseItemsDTO.Warehouse,
                    ItemsInStock = 1
                }));
                
            }
            await _bll.SaveChangesAsync();
            return Ok(warehouseItemsDTO.Warehouse);
        }
        
    }
}
