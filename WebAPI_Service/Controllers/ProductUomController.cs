using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.Core.DataModels;
using WebAPI_Service.Core.Interfaces;
using WebAPI_Service.DTO;
using WebAPI_Service.Validators;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUomController : ControllerBase
    {
        private IUomRepository repository;

        public ProductUomController(IUomRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductUomDto>>> Get()
        {
            var listUom = await repository.GetUomAsync();
            var result = listUom.Select(x => x.Adapt<ProductUomDto>());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductUomDto>> Get([Required] int id)
        {
            var uom = await repository.GetUomAsync(id);
            if (uom == null)
            {
                return NotFound();
            }

            var getProductUomDto = uom.Adapt<ProductUomDto>();
            return Ok(getProductUomDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductUomDto>> Post(CreateUomDto uomDto)
        {
            var validator = new CreateUomValidator();
            var validatorRes = validator.Validate(uomDto);
            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var productUom = uomDto.Adapt<ProductUom>();
            var result = await repository.AddUomAsync(productUom);
            return Ok(result.Adapt<ProductUomDto>());
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(ProductUomDto uomDto)
        {
            var validator = new UomValidator();
            var validatorRes = validator.Validate(uomDto);
            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var productUom = uomDto.Adapt<ProductUom>();
            var result = await repository.UpdateUomAsync(productUom);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var result = await repository.DeleteUomAsync(id);
            return result
                ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
