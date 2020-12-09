using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PublicApi.DTO.v2;

namespace WebApp.ApiControllers._1._1
{
    /// <summary>
    /// Culture info
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.1")]
    public class CulturesController : ControllerBase
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizationOptions"></param>
        public CulturesController(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Get the list of supported cultures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CultureDTO>))]
        public ActionResult<IEnumerable<CultureDTO>> GetCultures()
        {
            var result = _localizationOptions.Value.SupportedUICultures
                .Select(c => new CultureDTO()
                {
                    Code = c.Name,
                    Name = c.NativeName,
                }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get the resource [entityName, {code, name}]
        /// </summary>
        /// <returns>List of cultures</returns>
        [HttpGet("resources")]
        public ActionResult<List<Culture>> GetResources()
        {
            var allResources = new List<Culture>();
            
            var sharedSet =
                Resources.Views.Shared._Layout.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "shared",
                CultureDTO = new List<CultureDTO>(sharedSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });
            
            
            var categorySet =
                Resources.Domain.Category.Category.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "category",
                CultureDTO = new List<CultureDTO>(categorySet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });
            
            var itemSet =
                Resources.Domain.Item.Item.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "item",
                CultureDTO = new List<CultureDTO>(itemSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });

            var itemInCartSet =
                Resources.Domain.ItemInCart.ItemInCart.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "itemInCart",
                CultureDTO = new List<CultureDTO>(itemInCartSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });
  
            var commonSet =
                Resources.Common.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "common",
                CultureDTO = new List<CultureDTO>(commonSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });

            var warehouseSet =
                Resources.Domain.Warehouse.Warehouse.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            
            allResources.Add(new Culture()
            {
                EntityName = "warehouse",
                CultureDTO = new List<CultureDTO>(warehouseSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });

            var brandSet =
                Resources.Domain.Brand.ResourceManager
                    .GetResourceSet(Thread.CurrentThread.CurrentUICulture,
                        true, true);
            allResources.Add(new Culture()
            {
                EntityName = "brand",
                CultureDTO = new List<CultureDTO>(brandSet.Cast<DictionaryEntry?>().Select(e => 
                    new CultureDTO() {Name = e!.Value.Value!.ToString()!, Code = e.Value.Key.ToString()!}))
            });
            
            
            return Ok(allResources);
        }
    }
}
