using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationContext dbContext;

        public ProductRepository(ApplicationContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            dbContext.Products.Add(product);
            return await dbContext.SaveChangesAsync() == 1
                ? product
                : null;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            dbContext.Update(product);
            return await dbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return false;
            }

            dbContext.Products.Remove(product);
            return await dbContext.SaveChangesAsync() == 1;
        }

        public async Task<int> GetTotalQuontityAsync(int productId)
        {
            return await dbContext.ProductMovementss.Where(x => x.ProductId == productId).SumAsync(x => x.Quantity);
        }

        public async Task<int> GetTotalQuontityAsync(int productId, DateTime datetime)
        {
            return await dbContext.ProductMovementss.Where(x => x.ProductId == productId && x.InsertDateTime <= datetime).SumAsync(x => x.Quantity);
        }

        public async Task<ProductMovements> AddMovementsAsync(ProductMovements productMovements)
        {
            dbContext.ProductMovementss.Add(productMovements);
            return await dbContext.SaveChangesAsync() == 1
                ? productMovements
                : null;
        }
    }
}
