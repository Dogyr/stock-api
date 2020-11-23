using Microsoft.EntityFrameworkCore;
using WebAPI_Service.Core.DataModels;

namespace WebAPI_Service.Tests
{
    public class FakeDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUom> ProductUoms { get; set; }
        public DbSet<ProductMovements> ProductMovementss { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "FakeDb");
        }
    }
}
