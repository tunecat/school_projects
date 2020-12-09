using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CartsController : Controller
    {
        private readonly IAppBLL _bll;

        public CartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _bll.CartService.AllAsync();
            return View(await appDbContext);
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.CartService
                .FirstOrDefaultAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

       
        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.CartService.FirstOrDefaultAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cart = await _bll.CartService.FirstOrDefaultAsync(id);
            await _bll.CartService.RemoveAsync(cart);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
