using System.Collections.Generic;

namespace WebAPI_Service.Models
{
    public class ProductUom
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public List<Product> Products { get; set; }
    }
}
