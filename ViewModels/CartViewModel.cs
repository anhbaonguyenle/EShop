namespace EShop.ViewModels
{
    public class CartViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductTotalPrice => ProductPrice * ProductQuantity;
    }
}
