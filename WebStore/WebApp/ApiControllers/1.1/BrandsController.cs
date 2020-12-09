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
    [ApiVersion( "1.1" )]
    [Authorize(Roles = "Admin, Publisher")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandsController : ControllerBase
        {
            private readonly IAppBLL _bll;
            private readonly BrandMapper _brandMapper;
    
            
            /// <summary>
            /// Admin / Publisher controller for brands modifying
            /// </summary>
            /// <param name="bll"></param>
            public BrandsController(IAppBLL bll)
            {
                _bll = bll;
                _brandMapper = new BrandMapper();
            }
    
            // GET: api/Brands
            /// <summary>
            /// Get Brands
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
            {
                var brands = User.UserRole().Contains("Admin") ? await _bll.BrandService.AllAsync() : await _bll.BrandService.AllAsync(User.UserGuidId());
                return Ok(brands.Select<BLL.App.DTO.Brand, Brand>(b => _brandMapper.Map(b)));

            }
    
            // GET: api/Brands/5
            /// <summary>
            /// Get Brand by Id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet("{id}")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<ActionResult<Brand>> GetBrand(Guid id)
            {

                var brand = User.UserRole().Contains("Admin")
                    ? await _bll.BrandService.FirstOrDefaultAsync(id)
                    : await _bll.BrandService.FirstOrDefaultAsync(id, User.UserGuidId());
                
                if (brand == null) 
                {
                    return NotFound();
                }
                
                // mapping
                var result = _brandMapper.Map(brand);
                
                return Ok(result);
            }
    
            // PUT: api/Brands/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            /// <summary>
            /// Edit brand
            /// </summary>
            /// <param name="id"></param>
            /// <param name="brandEditDTO"></param>
            /// <returns></returns>
            [HttpPut("{id}")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<IActionResult> PutBrand(Guid id, BrandEdit brandEditDTO)
            {
            
                if (id != brandEditDTO.Id)
                {
                    return BadRequest();
                }
                
                var brand = User.UserRole().Contains("Admin")
                    ? await _bll.BrandService.FirstOrDefaultAsync(id)
                    : await _bll.BrandService.FirstOrDefaultAsync(id, User.UserGuidId());

                brand.UserId = User.UserGuidId();
                brand.Id = brandEditDTO.Id;
                brand.Name = brandEditDTO.Name;
                brand.Description = brandEditDTO.Description;

                await _bll.BrandService.UpdateAsync(brand);
                await _bll.SaveChangesAsync();
                return Ok();
            }
    
            // POST: api/Brands
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            /// <summary>
            /// Create brand
            /// </summary>
            /// <param name="brandCreateDTO"></param>
            /// <returns></returns>
            [HttpPost]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<ActionResult<Brand>> PostBrand(BrandCreate brandCreateDTO)
            {               
                brandCreateDTO.UserId = User.UserGuidId();
                var brand = _brandMapper.BrandCreate(brandCreateDTO);
                _bll.BrandService.Add(brand);
                await _bll.SaveChangesAsync();
    
                return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
            }
    
            // DELETE: api/Brands/5
            /// <summary>
            /// delete brand
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpDelete("{id}")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<ActionResult<Brand>> DeleteBrand(Guid id)
            {
                var brand = User.UserRole().Contains("Admin")
                    ? await _bll.BrandService.FirstOrDefaultAsync(id)
                    : await _bll.BrandService.FirstOrDefaultAsync(id, User.UserGuidId());
                
                if (brand == null) 
                {
                    return NotFound();
                }

                await _bll.BrandService.RemoveAsync(brand);
                await _bll.SaveChangesAsync();
    
                return Ok();
            }
        }
    }