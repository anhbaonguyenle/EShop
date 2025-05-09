using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public double ProductPrice { get; set; }
    }
    public class ProductDetailViewModel
    {
        public int ProductDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int AuthorId { get; set; }
        public string? ProductLongDescription { get; set; }
        public string? ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public double ProductPriceSale { get; set; }
        public int ProductQuantity { get; set; }
        public float Rate { get; set; }
        public int SaleStatus { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
