using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BrandsViewController : Controller
    {
        private readonly IAppBLL _bll;

        public BrandsViewController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: BrandsView
        public async Task<IActionResult> Index()
        {
            return View(await _bll.BrandService.AllAsync());
        }
        
        // GET: BrandsView/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _bll.BrandService
                .FirstOrDefaultAsync((Guid)id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }
    }
}