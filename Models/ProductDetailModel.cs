using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    [Table("ProductDetail")]
    public class ProductDetailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDetailID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual ProductModel Product { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AuthorModel Author { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual CategoryModel Category { get; set; }

        [StringLength(255)]
        public string? ProductLongDescription { get; set; }

        [StringLength(50)]
        public string? ProductImage { get; set; }

        public double ProductPrice { get; set; }
        public double ProductPriceSale { get; set; }
        public int ProductQuantity { get; set; }
        public float Rate { get; set; }
        public int SaleStatus{ get; set; } //1: sale, 0: not sale
    }
}

