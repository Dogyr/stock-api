using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.Core.DataModels;
using WebAPI_Service.Core.Interfaces;

namespace WebAPI_Service.Tests
{
    public class FakeUomRepository : IUomRepository
    {
        private FakeDbContext FakeContext;

        public FakeUomRepository(FakeDbContext context)
        {
            FakeContext = context;

            if (!FakeContext.ProductUoms.Any())
            {
                FakeContext.ProductUoms.Add(new ProductUom { Title = "шт" });
                FakeContext.ProductUoms.Add(new ProductUom { Title = "г" });
                FakeContext.ProductUoms.Add(new ProductUom { Title = "кг" });
                FakeContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<ProductUom>> GetUomAsync()
        {
            return await FakeContext.ProductUoms.ToListAsync();
        }

        public Task<ProductUom> GetUomAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductUom> AddUomAsync(ProductUom uom)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUomAsync(ProductUom uom)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUomAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
