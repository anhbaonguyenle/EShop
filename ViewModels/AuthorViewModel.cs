using System.Net;

namespace EShop.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorNation { get; set; }
        public string? AuthorDescription { get; set; }
        public string? AuthorImage { get; set; }
    }
}
