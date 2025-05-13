namespace EShop.ViewModels
{
    public class CheckoutViewModel
    {
        public bool CheckCustomer { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Note { get; set; }
    }
}
