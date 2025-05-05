using Microsoft.AspNetCore.Mvc;
using EShop.ViewModels;

namespace EShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly EShopContext db;
        public ProductController(EShopContext context)
        {
            db = context;
        }

        public IActionResult Index(int? id)
        {
            var products = db.ProductModels.AsQueryable();
            if(id.HasValue)
            {
                products = products.Where(p => p.ProductID == id.Value);
            }

            var result = products.Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryID = p.CategoryID,
                CategoryName = p.Category.CategoryName,
                ProductDescription = p.ProductDescription,
                ProductImage = p.ProductImage,
                ProductPrice = p.ProductPrice
            }).ToList();
            return View(result);
        }
    }
}
