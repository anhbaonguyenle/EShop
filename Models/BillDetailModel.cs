using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    [Table("BillDetail")]

    public class BillDetailModel
    {
        [Key]
        public int BillDetailId { get; set; }
        public int BillId { get; set; }
        public int ProductID { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public virtual BillModel BillIdNavigation { get; set; } = null!;
        public virtual ProductModel ProductIDNavigation { get; set; } = null!;

    }
}
