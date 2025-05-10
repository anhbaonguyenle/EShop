using EShop.Helpers;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace EShop.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var CartCount = HttpContext.Session.Get<List<CartViewModel>>(Setting.CartKey) ?? new List<CartViewModel>();

            return View("_Index", new CartSupportViewModel
            {
                Quantity = CartCount.Sum(p => p.ProductQuantity),
                TotalPrice = CartCount.Sum(p => p.ProductTotalPrice)
            });
        }
    }
}
