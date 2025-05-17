using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
    public class RegisterViewModel
    {
        public string CustomerUserName { get; set; }

        public string CustomerPassword { get; set; }

        public string CustomerConfirmPassword { get; set; }

        public string CustomerFullName { get; set; }

        public string? CustomerAddress { get; set; }

        public string? CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }

    }
}
