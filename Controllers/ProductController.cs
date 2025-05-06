using Microsoft.AspNetCore.Mvc;
using EShop.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            });
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var productsdata = db.ProductModels
                .Include(p => p.Category)
                .SingleOrDefault(p => p.ProductID == id);
            if (productsdata == null)
            {
                TempData["Error"] = "Product not found";
                return Redirect("/404");
            }
            var result = new ProductViewModel
            {
                ProductID = productsdata.ProductID,
                ProductName = productsdata.ProductName,
                CategoryID = productsdata.CategoryID,
                CategoryName = productsdata.Category.CategoryName,
                ProductDescription = productsdata.ProductDescription,
                ProductImage = productsdata.ProductImage,
                ProductPrice = productsdata.ProductPrice
            };
            return View(result);
        }
    }
}
