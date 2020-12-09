using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using com.nipetu.webstore.Contracts.BLL.Base;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ItemInWarehousesController : Controller
    {
        private readonly IAppBLL _bll;


        public ItemInWarehousesController(IAppBLL bll)
        {
            _bll = bll;
        }


        /// <summary>
        /// Comes from Warehouse view
        /// </summary>
        /// <param name="id">Item in warehouse Id</param>
        /// <param name="errorMessage">error Message</param>
        /// <returns>Item in warehouse info</returns>
        public async Task<IActionResult> Details(Guid? id, string? errorMessage = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInWarehouse = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id.Value);
            if (itemInWarehouse == null)
            {
                return NotFound();
            }
            
            var vm = new ItemInWarehouseViewModel()
            {
                ItemInWarehouse = itemInWarehouse,
                ErrorMessage = errorMessage
            };;

            return View(vm);
        }

        // GET: ItemInWarehouses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInWarehouse = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id.Value);
            if (itemInWarehouse == null)
            {
                return NotFound();
            }

            return View(itemInWarehouse);
        }

        // POST: ItemInWarehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemInWarehouse = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id);
            await _bll.ItemInWarehouseService.RemoveAsync(itemInWarehouse);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Warehouses", new {id = itemInWarehouse.WarehouseId});
        }
        
        [HttpPost, ActionName("ChangeAmount")]
        public async Task<IActionResult> ChangeAmount(Guid? id, int amount, string submitButton)
        {
            if (id == null) return NotFound();
            var item = await _bll.ItemInWarehouseService.FirstOrDefaultAsync(id.Value);

            switch (submitButton)
            {
                case "+":
                    item.ItemsInStock += amount;
                    break;
                case "-":
                    item.ItemsInStock -= amount;
                    if(item.ItemsInStock < 0) return RedirectToAction("Details", new{id = id, errorMessage = "Not enough products in stock"});
                    break;
                case "Empty":
                    item.ItemsInStock = 0;
                    break;
            }
            await _bll.ItemInWarehouseService.UpdateAsync(item);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", new{id = id});
        }
        
    }
}
