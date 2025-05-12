using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    [Table("Bill")]
    public class BillModel
    {
        [Key]
        public int BillId { get; set; }

        public string CustomerUserName { get; set; } = null;

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string CustomerFullName { get; set; }

        public string? CustomerAddress { get; set; } = null!;

        public int CustomerPhone { get; set; }

        public string PaymentMethods { get; set; } = null!;

        public string ShippingWay { get; set; } = null!;

        public double ShippingFee { get; set; }

        public int Status { get; set; }

        public string? Note { get; set; }

        public virtual ICollection<BillDetailModel> BillDetails { get; set; } = new List<BillDetailModel>();

        public virtual CustomerModel CustomerUserNameNavigation { get; set; } = null!;

    }
}
