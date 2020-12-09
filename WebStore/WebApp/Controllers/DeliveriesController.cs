using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class DeliveriesController : Controller
    {
        private readonly IAppBLL _bll;


        public DeliveriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index()
        {
            return View(await _bll.DeliveryService.AllAsync(User.UserGuidId()));
        }
        
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _bll.DeliveryService.AllAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var delivery = User.UserRole().Contains("Admin") ? await _bll.DeliveryService.FirstOrDefaultAsync(id.Value) : await _bll.DeliveryService.FirstOrDefaultAsync(id.Value, User.UserGuidId());
 
            if (delivery == null)
            {
                return NotFound();
            }
            
            var deliveryItems = delivery.ItemDeliveries.Select(itemDelivery => itemDelivery.Item).ToList();

            var vm = new DeliveryViewModel()
            {
                Delivery = delivery,
                DeliveryItems = deliveryItems!
            };

           
            return View(vm);
        }

 


        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = User.UserRole().Contains("Admin") ? await _bll.DeliveryService.FirstOrDefaultAsync(id.Value) : await _bll.DeliveryService.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (delivery == null)
            {
                return NotFound();
            }
            var vm = new DeliveryViewModel()
            {
                Delivery = delivery,
                DeliveryTypesSelectList = new SelectList(
                    await _bll.DeliveryTypeService.AllAsync(), nameof(DeliveryType.Id), nameof(DeliveryType.Name)),
            };
            return View(vm);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Delivery delivery)
        {
            delivery.UserId = User.UserGuidId();

            if (id != delivery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.DeliveryService.UpdateAsync(delivery);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                return RedirectToAction(nameof(Index));
            }
            var vm = new DeliveryViewModel()
                        {
                            Delivery = delivery,
                            DeliveryTypesSelectList = new SelectList(
                                await _bll.DeliveryTypeService.AllAsync(), nameof(DeliveryType.Id), nameof(DeliveryType.Name)),
                        };
                        return View(vm);
        }

        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = User.UserRole().Contains("Admin") ? await _bll.DeliveryService.FirstOrDefaultAsync(id.Value) : await _bll.DeliveryService.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var delivery = User.UserRole().Contains("Admin") ? await _bll.DeliveryService.FirstOrDefaultAsync(id) : await _bll.DeliveryService.FirstOrDefaultAsync(id, User.UserGuidId());
            if (delivery != null)
            {
                await _bll.DeliveryService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();

        }
     
    }
}
