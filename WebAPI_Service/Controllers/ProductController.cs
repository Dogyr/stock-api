using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAPI_Service.Models;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ApplicationContext dbContext;

        public ProductController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([Required] int id)
        {
            Product product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product == null
                ? (ActionResult)NotFound()
                : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
