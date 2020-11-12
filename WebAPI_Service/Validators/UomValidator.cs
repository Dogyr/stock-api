using FluentValidation;
using WebAPI_Service.DTO;

namespace WebAPI_Service.Validators
{
    public class UomValidator : AbstractValidator<CreateUomDto>
    {
        public UomValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Matches("^[А-ЯЁа-яё]+$");
        }
    }
}
