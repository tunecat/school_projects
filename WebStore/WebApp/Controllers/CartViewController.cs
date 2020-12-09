using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Maksekeskus;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.ViewModels;
using Delivery = BLL.App.DTO.Delivery;

namespace WebApp.Controllers
{
    public class CartViewController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly AppDbContext _db;
        

        public CartViewController(IAppBLL bll, AppDbContext db)
        {
            _bll = bll;
            _db = db;
        }

        /// <summary>
        /// Get all user's cart items
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            var cart = await _bll.CartService.GetCartViewAsync(User.UserGuidId());
            return View(cart);
        }


        /// <summary>
        ///  Clear cart confirmation
        /// </summary>
        /// <param name="cartId">Cart Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Clear(Guid? cartId)
        {
            if (cartId == null) return NotFound();
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());
            return View(cart);
        }

        /// <summary>
        /// Clear cart items
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Clear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart(Guid id)
        {
            await _bll.ItemInCartService.RemoveAllCartItems(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Item in cart have attribute 'amount', increase item amount
        /// </summary>
        /// <param name="id">item in cart Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> IncreaseQuantity(Guid? id)
        {
            if (id == null) return NotFound();
            await _bll.ItemInCartService.IncreaseQuantity(id.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Item in cart have attribute 'amount', decrease item amount
        /// </summary>
        /// <param name="id">item in cart Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> DecreaseQuantity(Guid? id)
        {
            if (id == null) return NotFound();
            await _bll.ItemInCartService.DecreaseQuantity(id.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="id">item in cart Id</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveProduct(Guid? id)
        {
            if (id == null) return NotFound();
            await _bll.ItemInCartService.RemoveAsync(id.Value, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get information about item
        /// </summary>
        /// <param name="id">item Id</param>
        /// <returns>Item Details page</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.ItemService.FirstOrDefaultAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return Redirect($"../../itemsView/Details/{id}");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns></returns>
        public async Task<IActionResult> ChooseDeliveryType(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var vm = new CartDeliveryViewModel
            {
                Cart = await _bll.CartService.GetCartAsync(User.UserGuidId()),
                DeliveryTypes = await _bll.DeliveryTypeService.AllAsync()
            };
            
            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId">cart ID</param>
        /// <param name="typeId">delivery type ID</param>
        /// <returns></returns>
        public async Task<IActionResult> ToPay(Guid? cartId, Guid? typeId)
        {
            if (cartId == null || typeId == null)
            {
                return NotFound();
            }

            var makseKeskus = new Maksekeskus();
            
            var vm = new CartDeliveryViewModel
            {
                PaymentMethods = await makseKeskus.GetPaymentMethods(),
                Cart = await _bll.CartService.GetCartAsync(User.UserGuidId()),
                DeliveryType = await _bll.DeliveryTypeService.FirstOrDefaultAsync(typeId.Value)
            };
            
            return View(vm);
        }

        /// <summary>
        /// Get all information to pay and delivery
        /// </summary>
        /// <param name="cartId">Cart Id</param>
        /// <param name="typeId">DeliveryType Id</param>
        /// <param name="bank">bank name with maksekeskus url -> "{bank name};url"</param>
        /// <param name="omniva_selection_value1">Omniva destination</param>
        /// <param name="address">Address destination</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> Pay(Guid? cartId, Guid? typeId, string bank, string? omniva_selection_value1, string? address)
        {
            var bankName = bank.Split(";")[0].Replace("{", "").Replace("}", "");
            var url = bank.Split(";")[1];
            var destination = address ?? omniva_selection_value1;
            if (cartId == null || typeId == null || destination == null)
            {
                return NotFound();
            }
            
            var deliveryType = await _bll.DeliveryTypeService.FirstOrDefaultAsync(typeId.Value);
            var cart = await _bll.CartService.GetCartAsync(User.UserGuidId());
            
            var delivery = new Delivery
            {
                Id = Guid.NewGuid(),
                Destination = destination,
                Cost = deliveryType.DeliveryCost,
                DeliveryTypeId = typeId.Value,
                UserId = User.UserGuidId(),
            };
            _bll.DeliveryService.CreateDelivery(delivery, cart.CartItems);
            
            var returnOkUrl = Url.PageLink().Contains("localhost") ? "https://localhost:5001/Deliveries"
                : "https://mamkupi.azurewebsites.net/DELIVERIES";
            
            var returnNotOkUrl = Url.PageLink().Contains("localhost") ? "https://localhost:5001"
                : "https://mamkupi.azurewebsites.net";

            var returnOk = new Dictionary<string, string>
            {
                ["url"] = returnOkUrl,
                ["method"] = "GET"
            };
            
            var returnNotOk = new Dictionary<string, string>
            {
                ["url"] = returnNotOkUrl,
                ["method"] = "GET"
            };

            var trans = new Dictionary<string, dynamic>
            {
                ["return_url"] = returnOk,
                ["cancel_url"] = returnNotOk,
                ["notification_url"] = returnNotOk,
            };
            
            var transactionDict = new Dictionary<string, dynamic>
            {
                ["amount"] = Math.Round((cart.TotalPrice + delivery.Cost), 2).ToString(),
                ["currency"] = "EUR",
                ["reference"] = cartId.ToString()!,
                ["merchant_data"] = "cart = " + cartId + " delivery " + delivery.Id,
                ["transaction_url"] = trans
            };
            var customerDict = new Dictionary<string, dynamic>
            {
                ["email"] = User.UserEmail(), ["country"] = "EE", ["locale"] = "et", ["ip"] = "80.235.22.114"
            };
            var transaction = new Dictionary<string, Dictionary<string, dynamic>>
            {
                ["transaction"] = transactionDict, ["customer"] = customerDict, ["Post"] = trans
            };

            var makseKeskus = new Maksekeskus();
            var transactionId = await makseKeskus.CreateTransaction(transaction);

            await _bll.CartService.UpdateStatusAsync(cart, CartStatuses.Paid.ToString());
            await _bll.SaveChangesAsync();
            
            var payment = new Payments()
            {
                UserId = User.UserGuidId(),
                PayedAt = DateTime.Now,
                Info = "user = " + User.UserGuidId() + "cart = " + cartId + " delivery " + delivery.Id,
                Sum = cart.TotalPrice + delivery.Cost,
                CartId = cartId.Value
            };

            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
            
            return Redirect(url + transactionId);
            
            // if success
            // await _bll.CartService.UpdateStatusAsync(cart, CartStatuses.Paid.ToString());
            // await _bll.SaveChangesAsync();
            
            // need something like bll.deliveryService.CreatePayments();
            // return Redirect($"../../deliveries/Index");
        }
    }
}