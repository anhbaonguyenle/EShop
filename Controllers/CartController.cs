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
        public List<CartViewModel> Cart
        {
            get
            {
                return HttpContext.Session.Get<List<CartViewModel>>(Setting.CartKey) ?? new List<CartViewModel>();
            }
        }
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
            var cart = Cart;
            if (cart.Count == 0)
            {
                TempData["Error"] = "Your cart is empty";
                return Redirect("/");
            }

            return View(Cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == Setting.Claim_UserId).Value;
                var customer = new CustomerModel();
                if (model.CheckCustomer)
                {
                    customer = db.CustomerModel.SingleOrDefault(kh => kh.CustomerUserName == customerId);
                }

                var bill = new BillModel
                {
                    CustomerUserName = customerId,
                    CustomerFullName = model.Fullname ?? customer.CustomerFullName,
                    CustomerAddress = model.Address ?? customer.CustomerAddress,
                    CustomerPhone = model.PhoneNumber ?? customer.CustomerPhone,
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now,
                    PaymentMethods = "COD",
                    ShippingWay = "GRAB",
                    Status = 0,
                    Note = model.Note
                };

                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(bill);
                    db.SaveChanges();


                    var billDetails = Cart.Select(item => new BillDetailModel
                        {
                            BillId = bill.BillId,
                            ProductID = item.ProductID,
                            ProductPrice = item.ProductPrice,
                            ProductQuantity = item.ProductQuantity
                        }).ToList();

                    db.AddRange(billDetails);
                    db.SaveChanges();

                    HttpContext.Session.Set<List<CartViewModel>>(Setting.CartKey, new List<CartViewModel>());

                    return View("Success");
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
            }
            return View(Cart);
        }
        #endregion
    }
}
