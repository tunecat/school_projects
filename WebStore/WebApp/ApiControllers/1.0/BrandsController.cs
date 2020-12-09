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
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public BrandsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
        {
            return await _context.Brands
                .Where(b => b.UserId == User.UserGuidId())
                .Select(b => new Brand
            {
                Id = b.Id, Description = b.Description, Name = b.Name, ItemsCount = b.Items!.Count
            }).ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var brand = await _context.Brands
                .Where(b => b.UserId == User.UserGuidId())
                .Select(b => new Brand()
            {
                Id = b.Id, Description = b.Description, Name = b.Name, ItemsCount = b.Items!.Count
            }).FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null) 
            {
                return NotFound();
            }
            return Ok(brand);
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(Guid id, BrandEdit brandEditDTO)
        {
            if (id != brandEditDTO.Id)
            {
                return BadRequest();
            }

            var brand = await _context.Brands.FindAsync(brandEditDTO.Id);
            brand.Id = brandEditDTO.Id;
            brand.Name = brandEditDTO.Name;
            brand.Description = brandEditDTO.Description;
            _context.Brands.Update(brand);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Brand>> PostBrand(BrandCreate brandCreateDTO)
        {
            var brand = new Domain.Brand
            {
                UserId = User.UserGuidId(),
                Name = brandCreateDTO.Name,
                Description = brandCreateDTO.Description
            };
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return Ok(brand);
        }

        private bool BrandExists(Guid id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}