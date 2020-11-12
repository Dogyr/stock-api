using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Service.DTO;
using WebAPI_Service.Models;

namespace WebAPI_Service.Validators
{
    public class ProductMovementsValidator : AbstractValidator<CreateProductMovementsDto>
    {
        public ProductMovementsValidator(ApplicationContext dbContext)
        {
            RuleFor(x => x)
                .MustAsync((x, c) => ValidateQuontityAsync(x, dbContext));
        }

        private async Task<bool> ValidateQuontityAsync(CreateProductMovementsDto dto, ApplicationContext dbContext)
        {
            var sumQuantity = await dbContext.ProductMovementss.Where(x => x.ProductId == dto.ProductId).SumAsync(x => x.Quantity);

            return dto.Quantity + sumQuantity >= 0;
        }

    }
}
