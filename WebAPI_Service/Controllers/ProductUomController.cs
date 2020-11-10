using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUomController : ControllerBase
    {
        ApplicationContext dbContext;

        public ProductUomController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductUom>>> Get()
        {
            return await dbContext.ProductUoms.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductUom>> Get([Required] int id)
        {
            ProductUom uom = await dbContext.ProductUoms.FirstOrDefaultAsync(x => x.Id == id);
            return uom == null
                ? (ActionResult)NotFound()
                : Ok(uom);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductUom>>> Post([FromBody] ProductUom uom)
        {
            if (uom == null)
            {
                return BadRequest();
            }

            dbContext.ProductUoms.Add(uom);
            await dbContext.SaveChangesAsync();
 
            return Ok(uom);
        }
    }
}
