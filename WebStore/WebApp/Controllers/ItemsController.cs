using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Publisher")]
    public class ItemsController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return User.UserRole().Contains("Admin")
                ? View(await _bll.ItemService.AllAsync())
                : View(await _bll.ItemService.AllAsync(User.UserGuidId()));
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item;
            if (User.UserRole().Contains("Admin"))
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id);
            }
            else
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id, User.UserGuidId());
            }

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemCreateEditViewModel()
            {
                CategorySelectList = new MultiSelectList(
                    await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id), nameof(Category.Name)),
                BrandSelectList = new SelectList(
                    await _bll.BrandService.AllAsync(User.UserGuidId()), nameof(Brand.Id), nameof(Brand.Name)),
            };
            return View(vm);
        }


        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateEditViewModel vm)
        {
            vm.Item.UserId = User.UserGuidId();
            if (ModelState.IsValid && vm.Categories != null)
            {
                var item = vm.Item;
                item.Id = Guid.NewGuid();
                item.Categories = vm.Categories.ToArray();
                _bll.ItemService.Add(item);


                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.CategorySelectList = new MultiSelectList(
                await _bll.CategoryService.GetAllSubCategoriesAsync(), nameof(Category.Id), nameof(Category.Name));
            vm.BrandSelectList = new SelectList(
                await _bll.BrandService.AllAsync(User.UserGuidId()), nameof(Brand.Id), nameof(Brand.Name));
            return View(vm);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item;
            if (User.UserRole().Contains("Admin"))
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id);
            }
            else
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id, User.UserGuidId());
            }

            if (item == null)
            {
                return NotFound();
            }

            var vm = new ItemCreateEditViewModel()
            {
                Item = item,
                CategorySelectList = new SelectList(await _bll.CategoryService.GetAllSubCategoriesAsync(), "Id", "Name",
                    item.BrandId),
                BrandSelectList = new SelectList(await _bll.BrandService.AllAsync(User.UserGuidId()), "Id", "Name",
                    item.BrandId)
            };

            return View(vm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemCreateEditViewModel vm)
        {
            var item = vm.Item;
            item.UserId = (await _bll.ItemService
                .FirstOrDefaultAsync(id)).UserId;

            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.ItemService.UpdateAsync(item);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _bll.ItemService.ExistsAsync(item.Id)))
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

            vm.CategorySelectList = new SelectList(await _bll.CategoryService.GetAllSubCategoriesAsync(), "Id", "Name",
                item.BrandId);
            vm.BrandSelectList = new SelectList(await _bll.BrandService.AllAsync(User.UserGuidId()), "Id", "Name",
                item.BrandId);
            return View(vm);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item;
            if (User.UserRole().Contains("Admin"))
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id);
            }
            else
            {
                item = await _bll.ItemService
                    .FirstOrDefaultAsync((Guid) id, User.UserGuidId());
            }

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var removeAsync = !User.UserRole().Contains("Admin")
                ? await _bll.ItemService.RemoveAsync(id, User.UserGuidId())
                : await _bll.ItemService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 
        public async Task<IActionResult> ChangeDiscount(Guid? id, int discount)
        {
            if (id == null) return NotFound();
            var item = await _bll.ItemService.FirstOrDefaultAsync(id.Value);
            item.Discount = discount;
            await _bll.ItemService.UpdateAsync(item);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new {id = id});
        }
    }
}