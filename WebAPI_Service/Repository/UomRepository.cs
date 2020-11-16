using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Repository
{
    public class UomRepository : IUomRepository
    {
        private ApplicationContext dbContext;

        public UomRepository(ApplicationContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<ProductUom>> GetUomAsync()
        {
            return await dbContext.ProductUoms.ToListAsync();
        }

        public async Task<ProductUom> GetUomAsync(int id)
        {
            return await dbContext.ProductUoms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductUom> AddUomAsync(ProductUom uom)
        {
            dbContext.ProductUoms.Add(uom);
            return await dbContext.SaveChangesAsync() == 1
                ? uom
                : null;
        }

        public async Task<bool> UpdateUomAsync(ProductUom uom)
        {
            dbContext.Update(uom);
            return await dbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteUomAsync(int id)
        {
            ProductUom uom = await dbContext.ProductUoms.FirstOrDefaultAsync(x => x.Id == id);

            if (uom == null)
            {
                return false;
            }

            dbContext.ProductUoms.Remove(uom);
            return await dbContext.SaveChangesAsync() == 1;
        }
    }
}
