using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string CustommerUsername { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string CustommerPassword { get; set; }
    }
}
