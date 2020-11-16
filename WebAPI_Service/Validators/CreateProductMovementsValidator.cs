using FluentValidation;
using System.Threading.Tasks;
using WebAPI_Service.DTO;
using WebAPI_Service.Repository;

namespace WebAPI_Service.Validators
{
    public class CreateProductMovementsValidator : AbstractValidator<CreateProductMovementsDto>
    {
        public CreateProductMovementsValidator(IProductRepository repository)
        {
            RuleFor(x => x)
                .MustAsync((x, c) => ValidateQuontityAsync(x, repository));
        }

        private async Task<bool> ValidateQuontityAsync(CreateProductMovementsDto dto, IProductRepository repository)
        {
            var sumQuantity = await repository.GetTotalQuontityAsync(dto.ProductId);
            return dto.Quantity + sumQuantity >= 0;
        }
    }
}
