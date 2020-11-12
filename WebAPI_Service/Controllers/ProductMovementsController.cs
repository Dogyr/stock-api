using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.DTO;
using WebAPI_Service.Models;
using WebAPI_Service.Validators;

namespace WebAPI_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMovementsController : ControllerBase
    {
        private ApplicationContext dbContext;

        public ProductMovementsController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductMovementsDto>>> Get()
        {
            return await dbContext.ProductMovementss.ProjectToType<GetProductMovementsDto>().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetProductMovementsDto>>> Post([FromBody] CreateProductMovementsDto createProductMovementsDto)
        {
            var sumQuantity = await dbContext.ProductMovementss.Where(x => x.ProductId == createProductMovementsDto.ProductId).SumAsync(x => x.Quantity);

            var validator = new ProductMovementsValidator(dbContext);
            var validatorRes = validator.Validate(createProductMovementsDto);

            if (!validatorRes.IsValid)
            {
                return BadRequest();
            }

            var productMovements = createProductMovementsDto.Adapt<ProductMovements>();
            productMovements.InsertDateTime = DateTime.Now;

            dbContext.ProductMovementss.Add(productMovements);
            await dbContext.SaveChangesAsync();

            var result = productMovements.Adapt<GetProductMovementsDto>();
            return Ok(createProductMovementsDto);
        }

        [HttpGet("{productId}/fromdate/{fromDate}")]
        public async Task<ActionResult<GetProductMovementsDto>> Get([Required] int productId, [Required] DateTime fromDate)
        {
            int? sum = await dbContext.ProductMovementss.Where(x => x.InsertDateTime <= fromDate && x.ProductId == productId).SumAsync(x => x.Quantity);
            return sum == null
                ? (ActionResult)NotFound()
                : Ok(sum);
        }
    }
}
