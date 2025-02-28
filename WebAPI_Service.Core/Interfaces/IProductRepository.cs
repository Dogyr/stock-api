﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Service.Core.DataModels;

namespace WebAPI_Service.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductAsync();

        Task<Product> GetProductAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<int> GetTotalQuontityAsync(int productId);

        Task<int> GetTotalQuontityAsync(int productId, DateTime datetime);

        Task<ProductMovements> AddMovementsAsync(ProductMovements productMovements);
    }
}
