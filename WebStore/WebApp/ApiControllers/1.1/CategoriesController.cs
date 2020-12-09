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
using PublicApi.DTO.v2;
using PublicApi.DTO.Mappers;

namespace WebApp.ApiControllers._1._1
{
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Publisher")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _categoryMapper;

        /// <summary>
        /// Categories controller
        /// </summary>
        /// <param name="bll"></param>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
            _categoryMapper = new CategoryMapper();
        }

        // GET: api/Categories
        /// <summary>
        /// Gett all parent categories for display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var parentCategories = await _bll.CategoryService.GetAllParentCategoriesAsync();
            var result = parentCategories.Select(c => _categoryMapper.Map(c));
            return Ok(result);
        }

        [HttpGet("Child")]
        public async Task<ActionResult<IEnumerable<Category>>> GetChildCategory()
        {
            var parentCategories = await _bll.CategoryService.GetAllSubCategoriesAsync();
            var result = parentCategories.Select(c => _categoryMapper.Map(c));
            return Ok(result);
        }


        // GET: api/Categories/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _bll.CategoryService.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_categoryMapper.Map(category));
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryEditDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryEdit categoryEditDTO)
        {
            if (id != categoryEditDTO.Id)
            {
                return BadRequest();
            }
            
            var category = _categoryMapper.CategoryEdit(categoryEditDTO);

            await _bll.CategoryService.UpdateAsync(category);
            await _bll.SaveChangesAsync();
            
            return Ok();
        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryCreate categoryCreateDTO)
        {
            var category = _categoryMapper.CategoryCreate(categoryCreateDTO);
            _bll.CategoryService.Add(category);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new {id = category.Id}, category);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await _bll.CategoryService.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _bll.CategoryService.RemoveAsync(category);
            await _bll.SaveChangesAsync();

            return Ok(category);
        }
    }
}