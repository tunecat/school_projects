using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemInWarehousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemInWarehousesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemInWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemInWarehouse>>> GetItemInWarehouse()
        {
            return await _context.ItemInWarehouses
                .Select(iw => new ItemInWarehouse()
                {
                    Id = iw.Id,
                    ItemId = iw.ItemId,
                    WarehouseId = iw.WarehouseId,
                    ItemsInStock = iw.ItemsInStock
                })
                .ToListAsync();
        }

        // GET: api/ItemInWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInWarehouse>> GetItemInWarehouse(Guid id)
        {
            var itemInWarehouse = await _context.ItemInWarehouses
                .Select(iw => new ItemInWarehouse()
                {
                    Id = iw.Id,
                    ItemId = iw.ItemId,
                    WarehouseId = iw.WarehouseId,
                    ItemsInStock = iw.ItemsInStock
                }).FirstOrDefaultAsync(iw => iw.Id == id);

            if (itemInWarehouse == null)
            {
                return NotFound();
            }

            return itemInWarehouse;
        }

        // PUT: api/ItemInWarehouses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemInWarehouse(Guid id, ItemInWarehouseEdit itemInWarehouseEditDTO)
        {
            if (id != itemInWarehouseEditDTO.Id)
            {
                return BadRequest();
            }

            var itemInWarehouse = await _context.ItemInWarehouses.FindAsync(itemInWarehouseEditDTO.Id);

            itemInWarehouse.ItemId = itemInWarehouseEditDTO.ItemId;
            itemInWarehouse.WarehouseId = itemInWarehouseEditDTO.WarehouseId;
            itemInWarehouse.ItemsInStock = itemInWarehouseEditDTO.ItemsInStock;
            _context.ItemInWarehouses.Update(itemInWarehouse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemInWarehouseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemInWarehouses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemInWarehouse>> PostItemInWarehouse(
            ItemInWarehouseCreate itemInWarehouseCreate)
        {
            var itemInWarehouse = new Domain.ItemInWarehouse
            {
                ItemId = itemInWarehouseCreate.ItemId,
                WarehouseId = itemInWarehouseCreate.WarehouseId,
                ItemsInStock = itemInWarehouseCreate.ItemsInStock
            };
            _context.ItemInWarehouses.Add(itemInWarehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemInWarehouse", new {id = itemInWarehouse.Id}, itemInWarehouse);
        }

        // DELETE: api/ItemInWarehouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemInWarehouse>> DeleteItemInWarehouse(Guid id)
        {
            var itemInWarehouse = await _context.ItemInWarehouses.FindAsync(id);
            if (itemInWarehouse == null)
            {
                return NotFound();
            }

            _context.ItemInWarehouses.Remove(itemInWarehouse);
            await _context.SaveChangesAsync();

            return Ok(itemInWarehouse);
        }

        private bool ItemInWarehouseExists(Guid id)
        {
            return _context.ItemInWarehouses.Any(e => e.Id == id);
        }

     
    }
}