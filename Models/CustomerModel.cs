using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
        [Key]
        [StringLength(18)]
        public string CustomerUserName { get; set; }

        [Required]
        [StringLength(500)]
        public string CustomerPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerFullName { get; set; }

        [StringLength(255)]
        public string? CustomerAddress { get; set; }

        [StringLength(50)]
        public string? CustomerImage { get; set; }

        [StringLength(50)]
        public string? CustomerEmail { get; set; }
        [Required]
        [Range(1000000000, 9999999999)]
        public int CustomerPhone { get; set; }
    }
}
