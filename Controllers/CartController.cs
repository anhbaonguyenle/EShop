using Microsoft.AspNetCore.Mvc;
using EShop.Helpers;
using EShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using EShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace EShop.Controllers
{
    public class CartController : Controller
    {
        private readonly EShopContext db;

        public CartController(EShopContext context)
        {
            db = context;
        }
        public List<CartViewModel> Cart => HttpContext.Session.Get<List<CartViewModel>>(Setting.CartKey) ?? new List<CartViewModel>();
        #region CartPage
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddtoCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductID == id);
            if (item == null)
            {
                var product = db.ProductDetailModels.SingleOrDefault(p => p.ProductID == id);
                if (product == null)
                {
                    TempData["Error"] = "Product not found";
                    return Redirect("/404");
                }
                else
                {
                    item = new CartViewModel
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ProductImage = product.ProductImage,
                        ProductPrice = product.ProductPrice,
                        ProductQuantity = quantity
                    };
                    cart.Add(item);
                }
            }
            else
            {
                item.ProductQuantity += quantity;
            }
            HttpContext.Session.Set(Setting.CartKey, cart);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductID == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(Setting.CartKey, cart);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Checkout
        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
                return Redirect("/");

            ViewBag.Cart = Cart;
            return View(new CheckoutViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            ViewBag.Cart = Cart;

            if (Cart.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(model);
            }

            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == Setting.Claim_UserId)?.Value;
            CustomerModel? customer = null;
            if (model.CheckCustomer && !string.IsNullOrWhiteSpace(customerId))
            {
                customer = db.CustomerModel.SingleOrDefault(kh => kh.CustomerUserName == customerId);
            }

            // Validate and resolve required fields
            var fullName = !string.IsNullOrWhiteSpace(model.Fullname) ? model.Fullname : customer?.CustomerFullName;
            var address = !string.IsNullOrWhiteSpace(model.Address) ? model.Address : customer?.CustomerAddress;
            var phone = !string.IsNullOrWhiteSpace(model.PhoneNumber) ? model.PhoneNumber : customer?.CustomerPhone;

            if (string.IsNullOrWhiteSpace(fullName))
                ModelState.AddModelError("Fullname", "Full name is required.");
            if (string.IsNullOrWhiteSpace(address))
                ModelState.AddModelError("Address", "Address is required.");
            if (string.IsNullOrWhiteSpace(phone))
                ModelState.AddModelError("PhoneNumber", "Phone number is required.");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var bill = new BillModel
            {
                CustomerUserName = customerId,
                CustomerFullName = fullName,
                CustomerAddress = address,
                CustomerPhone = phone,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                PaymentMethods = "COD",
                ShippingWay = "GRAB",
                Status = 0,
                Note = model.Note
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.BillModel.Add(bill);
                    db.SaveChanges();

                    var billDetails = Cart.Select(item => new BillDetailModel
                    {
                        BillId = bill.BillId,
                        ProductID = item.ProductID,
                        ProductPrice = item.ProductPrice,
                        ProductQuantity = item.ProductQuantity
                    }).ToList();

                    db.BillDetailModel.AddRange(billDetails);
                    db.SaveChanges();

                    transaction.Commit();

                    // Clear cart after successful order
                    HttpContext.Session.Set<List<CartViewModel>>(Setting.CartKey, new List<CartViewModel>());

                    return View("Success");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "An error occurred while processing your order. Please try again.");
                    ViewBag.Cart = Cart;
                    return View(model);
                }
            }
        }
        #endregion
    }
}
