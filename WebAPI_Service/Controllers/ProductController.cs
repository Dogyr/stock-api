using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAPI_Service.DTO;
using WebAPI_Service.Models;
using WebAPI_Service.Validators;

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
        public async Task<ActionResult<IEnumerable<GetProductDto>>> Get()
        {
            return await dbContext.Products.ProjectToType<GetProductDto>().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get([Required] int id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            var getProductById = product.Adapt<GetProductDto>();

            return  Ok(getProductById);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> Post([FromBody] CreateProductDto productDto)
        {
            var validator = new ProductValidator();
            var validatorRes = validator.Validate(productDto);

            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var product = productDto.Adapt<Product>();
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            var result = product.Adapt<GetProductDto>();
            return Ok(result);
        }
    }
}
