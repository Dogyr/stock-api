using Microsoft.EntityFrameworkCore;
using WebAPI_Service.Core.DataModels;

namespace WebAPI_Service.DataLayer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUom> ProductUoms { get; set; }
        public DbSet<ProductMovements> ProductMovementss { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Uom)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.UomId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductMovements>()
                .HasOne(p => p.Product)
                .WithMany(t => t.ProductMovements)
                .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
