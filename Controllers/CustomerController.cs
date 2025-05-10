using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using EShop.ViewModels;
using AutoMapper;
using EShop.Helpers;
using EShop.Models;

namespace EShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EShopContext db;
        private readonly IMapper _mapper;
        public CustomerController(EShopContext context, IMapper mapper)

        {
            db = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(RegisterViewModel model, IFormFile CusImage)
        {
            string imageFileURL = "CustomerDefault.png";

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.CustomerPassword != model.CustomerConfirmPassword)
                    {
                        ModelState.AddModelError("", "Passwords do not match.");
                        return View(model);
                    }

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.CustomerPassword);

                    var customer = _mapper.Map<CustomerModel>(model);
                    customer.CustomerPassword = hashedPassword;

                    if (CusImage != null && CusImage.Length > 0)
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", CusImage.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            CusImage.CopyTo(stream);
                        }
                        imageFileURL = CusImage.FileName;
                    }
                    customer.CustomerImage = imageFileURL;

                    db.CustomerModel.Add(customer);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while registering: " + ex.Message);
                }
            }

            return View(model);
        }

    }
}
