using Microsoft.AspNetCore.Mvc;
using EShop.Helpers;
using EShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using EShop.Models;
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
        public IActionResult AddtoCart(int id, int quantity =1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductID == id);
            if(item == null)
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
            if(item != null)
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
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all required fields";
                return Redirect("/Cart/Checkout");
            }
            else
            {
                var cusID = HttpContext.User.Claims.SingleOrDefault(p => p.Type == Setting.Claim_UserId);
                var cus = new CustomerModel();

                //var bill = new BillModel
                //{
                //    CustomerUserName = cusID 

                //};
                if (cusID == null)
                {
                    TempData["Error"] = "Customer not found";
                    return Redirect("/404");
                }
                else
                {

                }
            }
                return View(Cart);
        }
        #endregion

    }
}
