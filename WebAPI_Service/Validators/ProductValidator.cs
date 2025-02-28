﻿using FluentValidation;
using WebAPI_Service.DTO;

namespace WebAPI_Service.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[А-ЯЁа-яё0-9]+$");

            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.UomId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
