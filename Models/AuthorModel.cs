using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    [Table("Author")]
    public class AuthorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string AuthorName { get; set; }

        public string AuthorNation { get; set; }

        [StringLength(50)]
        public string? AuthorDescription { get; set; }
        [StringLength(1000)]
        public string? AuthorLongDescription { get; set; }
        public string AuthorImage { get; set; }
        public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();
    }
}
