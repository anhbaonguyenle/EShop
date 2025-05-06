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
}
