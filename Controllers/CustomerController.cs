using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
