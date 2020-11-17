using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebAPI_Service.DTO;
using WebAPI_Service.Core.Interfaces;
using WebAPI_Service.Core.DataModels;
using WebAPI_Service.Validators;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMovementsController : ControllerBase
    {
        private IProductRepository repository;

        public ProductMovementsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductMovementsDto>>> Post(CreateProductMovementsDto createProductMovementsDto)
        {
            var validator = new CreateProductMovementsValidator(repository);
            var validatorRes = validator.Validate(createProductMovementsDto);

            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var productMovements = createProductMovementsDto.Adapt<ProductMovements>();
            productMovements.InsertDateTime = DateTime.Now;

            var result = await repository.AddMovementsAsync(productMovements);
            return Ok(result.Adapt<ProductMovementsDto>());
        }

        [HttpGet("{productId}/fromdate/{fromDate}")]
        public async Task<ActionResult<ProductMovementsDto>> Get([Required] int productId, [Required] DateTime fromDate)
        {
            var sum = await repository.GetTotalQuontityAsync(productId, fromDate);
            return sum == 0
                ? (ActionResult)NotFound()
                : Ok(sum);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductMovementsDto>> Get([Required] int productId)
        {
            var sum = await repository.GetTotalQuontityAsync(productId);
            return sum == 0
                ? (ActionResult)NotFound()
                : Ok(sum);
        }
    }
}
