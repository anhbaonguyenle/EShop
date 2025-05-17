using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using EShop.ViewModels;
using AutoMapper;
using EShop.Helpers;
using EShop.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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
        #region Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var customer = db.CustomerModel.SingleOrDefault(c => c.CustomerUserName == model.CustommerUsername);
                if (customer != null && BCrypt.Net.BCrypt.Verify(model.CustommerPassword, customer.CustomerPassword))
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, customer.CustomerEmail),
                        new Claim(ClaimTypes.Name, customer.CustomerFullName),
                        new Claim(Setting.Claim_UserId, customer.CustomerUserName),
                        new Claim(ClaimTypes.Role, "Customer")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid username or password.");
                }
            }
            return View();
        }
        #endregion
        #region Profile
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        #endregion
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.CustomerPassword != model.CustomerConfirmPassword)
            {
                ModelState.AddModelError("CustomerConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            var existingCustomer = db.CustomerModel.FirstOrDefault(c => c.CustomerUserName == model.CustomerUserName);
            if (existingCustomer != null)
            {
                ModelState.AddModelError("CustomerUserName", "Username is already taken.");
                return View(model);
            }

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.CustomerPassword);

                var customer = _mapper.Map<CustomerModel>(model);
                customer.CustomerPassword = hashedPassword;

                db.CustomerModel.Add(customer);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login", "Customer");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while registering: {ex.Message}");
                return View(model);
            }
        }

        #endregion

    }
}
