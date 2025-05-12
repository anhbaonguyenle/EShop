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
        public virtual DbSet<ProductDetailModel> ProductDetailModels { get; set; }
        public virtual DbSet<CategoryModel> CategoryModels { get; set; }
        public virtual DbSet<AuthorModel> AuthorModels { get; set; }
        public virtual DbSet<CustomerModel> CustomerModel { get; set; }
        public virtual DbSet<BillModel> BillModel { get; set; }
        public virtual DbSet<BillDetailModel> BillDetailModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDetailModel>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductDetailModel>()
                .HasOne(p => p.Author)
                .WithMany(a => a.ProductDetails)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductDetailModel>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
