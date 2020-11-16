using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Repository
{
    public interface IUomRepository 
    {
        Task<IEnumerable<ProductUom>> GetUomAsync();

        Task<ProductUom> GetUomAsync(int id);

        Task<ProductUom> AddUomAsync(ProductUom uom);

        Task<bool> UpdateUomAsync(ProductUom uom);

        Task<bool> DeleteUomAsync(int id);
    }
}
