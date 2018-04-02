using System;
using Domain.Entities;
using Domain.Interfaces.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class ItemValidator : AbstractValidator<IAmItem>, IAmDomainValidator
    {
        public ItemValidator()
        {
            RuleFor(m => m.Description).NotNull().NotEmpty().Length(1, 100);
        }
    }
}
