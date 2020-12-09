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
using PublicApi.DTO;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items
                //.Where(i => i.UserId == User.UserGuidId())
                .Select(i => new Item
                {
                    Name = i.Name,
                    Id = i.Id,
                    BrandId = i.BrandId,
                    Description = i.Description,
                    Discount = i.Discount,
                    Price = i.Price,
                    SeparatedWarehousesCount = i.ItemInWarehouses!.Count,
                })
                .ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _context.Items
                //.Where(i => i.UserId == User.UserGuidId())
                .Select(i => new Item
                {
                    Name = i.Name,
                    Id = i.Id,
                    BrandId = i.BrandId,
                    Description = i.Description,
                    Discount = i.Discount,
                    Price = i.Price,
                    SeparatedWarehousesCount = i.ItemInWarehouses!.Count
                }).FirstOrDefaultAsync(i => i.Id == id);


            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutItem(Guid id, ItemEdit itemEditDTO)
        {
            if (id != itemEditDTO.Id)
            {
                return BadRequest();
            }


            var item = await _context.Items.FindAsync(itemEditDTO.Id);
            item.Name = itemEditDTO.Name;
            item.Description = itemEditDTO.Description;
            item.Discount = itemEditDTO.Discount;
            item.Price = itemEditDTO.Price;
            _context.Items.Update(item);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<Item>> PostItem(ItemCreate itemCreateDTO)
        {
            var item = new Domain.Item
            {
                Name = itemCreateDTO.Name,
                UserId = User.UserGuidId(),
                BrandId = itemCreateDTO.BrandId,
                Description = itemCreateDTO.Description,
                Discount = itemCreateDTO.Discount,
                Price = itemCreateDTO.Price
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new {id = item.Id}, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Item>> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }


  
    }
}