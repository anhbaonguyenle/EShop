using Microsoft.EntityFrameworkCore;
using EShop.Models;
namespace EShop
{
    public class EShopContext : DbContext
    {
        public EShopContext()
        {
        }
        public EShopContext(DbContextOptions<EShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<CategoryModel> CategoryModels { get; set; }
    }
}
