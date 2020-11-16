using System.Collections.Generic;

namespace WebAPI_Service.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public int UomId { get; set; }

        public ProductUom Uom  { get; set; }

        public List<ProductMovements> ProductMovements { get; set; }
    }
}
