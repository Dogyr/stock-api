using System;

namespace WebAPI_Service.Models
{
    public class ProductMovements
    {
        public int Id { get; set; }

        public DateTime InsertDateTime { get; set; } 

        public int Quantity { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }
    } 
}
