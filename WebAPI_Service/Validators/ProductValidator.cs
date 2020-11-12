using FluentValidation;
using WebAPI_Service.DTO;

namespace WebAPI_Service.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[А-ЯЁа-яё0-9]+$");
        }
    }
}
