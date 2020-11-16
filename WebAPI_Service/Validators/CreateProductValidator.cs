using FluentValidation;
using WebAPI_Service.DTO;

namespace WebAPI_Service.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[А-ЯЁа-яё0-9]+$");

            RuleFor(x => x.UomId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
