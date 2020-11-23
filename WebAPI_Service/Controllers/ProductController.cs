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
    public class ProductController : ControllerBase
    {
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var listProduct = await repository.GetProductAsync();
            var result = listProduct.Select(x => x.Adapt<ProductDto>());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get([Required] int id)
        {
            var product = await repository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            var getProductById = product.Adapt<ProductDto>();
            return  Ok(getProductById);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(CreateProductDto productDto)
        {
            var validator = new CreateProductValidator();
            var validatorRes = validator.Validate(productDto);
            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var product = productDto.Adapt<Product>();
            var result = await repository.AddProductAsync(product);
            return Ok(product.Adapt<ProductDto>());
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(ProductDto productDto)
        {
            var validator = new ProductValidator();
            var validatorRes = validator.Validate(productDto);
            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var product = productDto.Adapt<Product>();
            var result = await repository.UpdateProductAsync(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([Required] int id)
        {
            return await repository.DeleteProductAsync(id)
                ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
