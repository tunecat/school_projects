using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using BLL.App.DTO;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Publisher")]
    public class BrandsController : Controller
    { 
        private readonly IAppBLL _bll;

        public BrandsController(IAppBLL bll)
        {
            _bll = bll;
        } 

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return User.UserRole().Contains("Admin") ? View(await _bll.BrandService.AllAsync()) : View(await _bll.BrandService.AllAsync(User.UserGuidId()));
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Brand brand;
            if (User.UserRole().Contains("Admin"))
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id);
            } else
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id, User.UserGuidId());
            }
            
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            brand.UserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                //brand.Id = Guid.NewGuid();
                brand.Test = "Test";
                _bll.BrandService.Add(brand);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brand brand;
            if (User.UserRole().Contains("Admin"))
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id);
            } else
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id, User.UserGuidId());
            }
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Brand brand)
        {
            brand.UserId = (await _bll.BrandService
                .FirstOrDefaultAsync(id)).UserId;
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.BrandService.UpdateAsync(brand);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.BrandService.ExistsAsync(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brand brand;
            if (User.UserRole().Contains("Admin"))
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id);
            } else
            {
                brand = await _bll.BrandService
                    .FirstOrDefaultAsync((Guid)id, User.UserGuidId());
            }
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var removeAsync = !User.UserRole().Contains("Admin")
                ? await _bll.BrandService.RemoveAsync(id, User.UserGuidId())
                : await _bll.BrandService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
