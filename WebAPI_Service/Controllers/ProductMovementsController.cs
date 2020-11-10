using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMovementsController : ControllerBase
    {
        ApplicationContext dbContext;

        public ProductMovementsController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMovements>>> Get()
        {
            return await dbContext.ProductMovementss.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductMovements>>> Post([FromBody] ProductMovements productMovements)
        {
            if (productMovements == null)
            {
                return BadRequest();
            }

            productMovements.InsertDateTime = DateTime.Now;
            dbContext.ProductMovementss.Add(productMovements);
            await dbContext.SaveChangesAsync();

            return Ok(productMovements);
        }

        [HttpGet("{productId}/fromdate/{fromDate}")]
        public async Task<ActionResult<ProductMovements>> Get([Required] int productId, [Required] DateTime fromDate)
        {
            int? sum = await dbContext.ProductMovementss.Where(x => x.InsertDateTime <= fromDate && x.ProductId == productId).SumAsync(x => x.Quantity);
            return sum == null
                ? (ActionResult)NotFound()
                : Ok(sum);
        }
    }
}
