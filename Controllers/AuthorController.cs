using EShop.Models;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
    public class AuthorController : Controller
    {
        private readonly EShopContext db;
        public AuthorController(EShopContext context)
        {
            db = context;
        }

        public IActionResult Index(int? id)
        {
            var query = db.AuthorModels.AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(p => p.AuthorId == id.Value);
            }

            var authors = query.Select(p => new AuthorViewModel
            {
                AuthorId = p.AuthorId,
                AuthorName = p.AuthorName,
                AuthorNation = p.AuthorNation,
                AuthorDescription = p.AuthorDescription,
                AuthorImage = p.AuthorImage,
            }).ToList();

            return View(authors);
        }

    }
}
