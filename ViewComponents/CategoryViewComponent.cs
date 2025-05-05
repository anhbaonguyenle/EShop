using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace EShop.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly EShopContext db;
        public CategoryViewComponent(EShopContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.CategoryModels.Select(lo => new CategoryViewModel
            {
                CategoryId = lo.CategoryId,
                CategoryName = lo.CategoryName,
                Count = lo.product.Count,
            }).OrderBy(p => p.CategoryName);
            return View("_CatagoryComponent", data);


        }
    }
}
