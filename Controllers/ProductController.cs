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
            var productDetail = db.ProductDetailModels
                .Include(p => p.Author)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == id);
            if (productDetail == null)
            {
                TempData["Error"] = "Product not found";
                return Redirect("/404");
            }
            var result = new ProductDetailViewModel
            {
                ProductDetailID = productDetail.ProductDetailID,
                ProductID = productDetail.ProductID,
                ProductName = productDetail.ProductName,
                AuthorId = productDetail.AuthorId,
                AuthorName = productDetail.Author.AuthorName,
                ProductLongDescription = productDetail.ProductLongDescription,
                CategoryName = productDetail.Category.CategoryName,
                ProductImage = productDetail.ProductImage,
                ProductPrice = productDetail.ProductPrice,
                ProductPriceSale = productDetail.ProductPriceSale,
                ProductQuantity = productDetail.ProductQuantity,
                Rate = productDetail.Rate,
                SaleStatus = productDetail.SaleStatus
            };
            return View(result);
        }
    }
}