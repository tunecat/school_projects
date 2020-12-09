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
    public class BrandsViewController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BrandMapper _brandMapper;
        
        /// <summary>
        /// Brands for customers
        /// </summary>
        /// <param name="bll"></param>
        public BrandsViewController(IAppBLL bll)
        {
            _bll = bll;
            _brandMapper = new BrandMapper();
        }

        // GET: api/Brands
        /// <summary>
        /// get all brands
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
        {
            var brands = await _bll.BrandService.AllAsync();
            return Ok(brands.Select<BLL.App.DTO.Brand, Brand>(b => _brandMapper.Map(b)));
        }
        
        // GET: api/Brands/5
        /// <summary>
        /// get brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {

            var brand = await _bll.BrandService.FirstOrDefaultAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            // mapping
            var result = _brandMapper.Map(brand);

            return Ok(result);
        }

    }
}