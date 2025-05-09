using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddtoCart(int id, int quantity)
        {
            return View();
        }
    }
}
