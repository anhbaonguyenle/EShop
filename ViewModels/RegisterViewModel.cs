using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(18, ErrorMessage = "Maximum 18 characters")]
        public string CustomerUserName { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(500)]
        public string CustomerPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(500)]
        public string CustomerConfirmPassword { get; set; }
        [Required]
        [MaxLength(255)]
        public string CustomerFullName { get; set; }
        [MaxLength(255)]
        public string? CustomerAddress { get; set; }
        [MaxLength(50)]
        public string? CustomerImage { get; set; }
        [MaxLength(50)]
        public string? CustomerEmail { get; set; }
        [Required]
        [Range(1000000000, 9999999999)]
        public int CustomerPhone { get; set; }

    }
}
