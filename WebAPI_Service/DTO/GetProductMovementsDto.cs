using System;

namespace WebAPI_Service.DTO
{
    public class GetProductMovementsDto
    {
        public int Id { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int Quantity { get; set; }

        public int? ProductId { get; set; }
    }
}
