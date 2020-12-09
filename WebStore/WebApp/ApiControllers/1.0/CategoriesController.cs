using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Categories
                .Select(c => new Category()
                {
                    Id = c.Id, 
                    Name = c.Name, 
                    ParentCategoryId = c.ParentCategoryId, 
                    CategoryItemsCount = c.CategoryItems!.Count, 
                    //ChildCategoriesCount = c.ChildCategories!.Count
                })
                .ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _context.Categories
                .Select(c => new Category()
                {
                    Id = c.Id, 
                    Name = c.Name, 
                    ParentCategoryId = c.ParentCategoryId, 
                    CategoryItemsCount = c.CategoryItems!.Count, 
                    //ChildCategoriesCount = c.ChildCategories!.Count
                }).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryEdit categoryEditDTO)
        {
            if (id != categoryEditDTO.Id)
            {
                return BadRequest();
            }
   
            var category = await _context.Categories.FindAsync(categoryEditDTO.Id);
            category.Name = categoryEditDTO.Name;
            category.ParentCategoryId = categoryEditDTO.ParentCategoryId;
            _context.Categories.Update(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryCreate categoryCreateDTO)
        {
            var category = new Domain.Category
            {
                Name = categoryCreateDTO.Name,
                ParentCategoryId = categoryCreateDTO.ParentCategoryId
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
