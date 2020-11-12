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
    public class ProductUomController : ControllerBase
    {
        private ApplicationContext dbContext;

        public ProductUomController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductUomDto>>> Get()
        {
            return await dbContext.ProductUoms.ProjectToType<GetProductUomDto>().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductUomDto>> Get([Required] int id)
        {
            var uom = await dbContext.ProductUoms.FirstOrDefaultAsync(x => x.Id == id);

            if (uom == null)
            {
                return NotFound();
            }

            var getProductUomDto = uom.Adapt<GetProductUomDto>();

            return Ok(getProductUomDto);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductUomDto>> Post([FromBody] CreateUomDto uom)
        {
            var validator = new UomValidator();
            var validatorRes = validator.Validate(uom);

            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var productUom = uom.Adapt<ProductUom>();

            dbContext.ProductUoms.Add(productUom);
            await dbContext.SaveChangesAsync();

            var result = productUom.Adapt<GetProductUomDto>();
            return Ok(result);
        }
    }
}
