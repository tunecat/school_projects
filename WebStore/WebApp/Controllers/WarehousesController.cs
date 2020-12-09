using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using BLL.App.DTO;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Publisher")]
    public class WarehousesController : Controller
    {
        private readonly IAppBLL _bll;

        public WarehousesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            return User.UserRole().Contains("Admin")
                ? View(await _bll.WarehouseService.AllAsync())
                : View(await _bll.WarehouseService.AllAsync(User.UserGuidId()));
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse warehouse;
            if (User.UserRole().Contains("Admin"))
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value);
            }
            else
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            }

            if (warehouse == null)
            {
                return NotFound();
            }

            var vm = new WarehouseItemsViewModel
            {
                Warehouse = warehouse
            };

            return View(vm);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            warehouse.UserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                //warehouse.Id = Guid.NewGuid();
                _bll.WarehouseService.Add(warehouse);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse warehouse;
            if (User.UserRole().Contains("Admin"))
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value);
            }
            else
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            }

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Warehouse warehouse)
        {
            warehouse.UserId = (await _bll.WarehouseService
                .FirstOrDefaultAsync(id)).UserId;
            if (id != warehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.WarehouseService.UpdateAsync(warehouse);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.WarehouseService.ExistsAsync(warehouse.Id))
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

            return View(warehouse);
        }


        // Get
        public async Task<IActionResult> AddItemsToWarehouse(Guid? warehouseId)
        {
            if (warehouseId == null) return NotFound();
            var warehouse = User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.FirstOrDefaultAsync(warehouseId.Value)
                : await _bll.WarehouseService.FirstOrDefaultAsync(warehouseId.Value, User.UserGuidId());

            var items = User.UserRole().Contains("Admin")
                ? await _bll.ItemService.AllAsync()
                : await _bll.ItemService.AllAsync(User.UserGuidId());
            
            // get list of items which not in warehouse
            items = items.Where(i =>
                {
                    var boole = true;
                    foreach (var warehouseItem in warehouse.WarehouseItems!)
                    {
                        if (i.Id == warehouseItem.ItemId)
                        {
                            boole = false;
                        }
                    }
                    return boole;
                }
            );
            var vm = new WarehouseItemsViewModel
            {
                Warehouse = warehouse,
                ItemsSelectList = new MultiSelectList(items, nameof(Item.Id), nameof(Item.Name))
            };
            return View(vm);
        }

        // Post
        [HttpPost, ActionName("AddItemsToWarehouse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItemsToWarehouse(WarehouseItemsViewModel vm)
        {
            foreach (var itemId in vm.Items!)
            {
                var newItem = _bll.ItemInWarehouseService.Add(new ItemInWarehouse
                {
                    Id = Guid.NewGuid(),
                    ItemId = itemId,
                    WarehouseId = vm.Warehouse.Id,
                    ItemsInStock = 1
                });
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", new {id = vm.Warehouse.Id});
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse warehouse;
            if (User.UserRole().Contains("Admin"))
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value);
            }
            else
            {
                warehouse = await _bll.WarehouseService
                    .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            }

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(new WarehouseItemsViewModel {Warehouse = warehouse});
        }


        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var removeAsync = !User.UserRole().Contains("Admin")
                ? await _bll.WarehouseService.RemoveAsync(id)
                : await _bll.WarehouseService.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}