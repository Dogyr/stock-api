using FluentValidation;
using WebAPI_Service.DTO;

namespace WebAPI_Service.Validators
{
    public class CreateUomValidator : AbstractValidator<CreateUomDto>
    {
        public CreateUomValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Matches("^[А-ЯЁа-яё]+$");
        }
    }
}
