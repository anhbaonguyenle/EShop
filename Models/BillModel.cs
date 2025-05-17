using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    [Table("Bill")]
    public class BillModel
    {
        [Key]
        public int BillId { get; set; }

        [Required]
        [StringLength(18)]
        public string CustomerUserName { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string CustomerFullName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string PaymentMethods { get; set; }
        public string ShippingWay { get; set; }
        public double ShippingFee { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<BillDetailModel> BillDetails { get; set; } = new List<BillDetailModel>();

        [ForeignKey(nameof(CustomerUserName))]
        public virtual CustomerModel Customer { get; set; }
    }
}