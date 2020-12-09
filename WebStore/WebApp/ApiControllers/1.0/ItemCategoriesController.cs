using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;

namespace WebApp.ApiControllers._1._0
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategory>>> GetItemCategory()
        {
            return await _context.ItemCategories
                .Select(ic => new ItemCategory()
                {
                    Id = ic.Id,
                    ItemId = ic.ItemId,
                    CategoryId = ic.CategoryId                 
                })
                .ToListAsync();
        }

        // GET: api/ItemCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCategory>> GetItemCategory(Guid id)
        {
            var itemCategory = await _context.ItemCategories.Select(ic => new ItemCategory()
            {
                Id = ic.Id,
                ItemId = ic.ItemId,
                CategoryId = ic.CategoryId
            }).FirstOrDefaultAsync(ic => ic.Id == id);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ItemCategories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCategory(Guid id, ItemCategoryEdit itemCategoryEditDTO)
        {
            if (id != itemCategoryEditDTO.Id)
            {
                return BadRequest();
            }


            var itemCategory =await _context.ItemCategories.FindAsync(itemCategoryEditDTO.Id);
            itemCategory.CategoryId = itemCategoryEditDTO.CategoryId;
            itemCategory.ItemId = itemCategoryEditDTO.ItemId;
            _context.ItemCategories.Update(itemCategory);
                
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCategoryExists(id))
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

        // POST: api/ItemCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ItemCategory>> PostItemCategory(ItemCategoryCreate itemCategoryCreateDTO)
        {
            var itemCategory = new Domain.ItemCategory
            {
                ItemId = itemCategoryCreateDTO.ItemId,
                CategoryId = itemCategoryCreateDTO.CategoryId
            };
            _context.ItemCategories.Add(itemCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCategory", new { id = itemCategory.Id }, itemCategory);
        }

        // DELETE: api/ItemCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemCategory>> DeleteItemCategory(Guid id)
        {
            var itemCategory = await _context.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            _context.ItemCategories.Remove(itemCategory);
            await _context.SaveChangesAsync();

            return Ok(itemCategory);
        }

        private bool ItemCategoryExists(Guid id)
        {
            return _context.ItemCategories.Any(e => e.Id == id);
        }
    }
}
