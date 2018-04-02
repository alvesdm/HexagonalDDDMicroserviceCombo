using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Shared.Results;
using FluentValidation;

namespace Infrastructure.Domain.Data
{
    public abstract class ValidatableRepositoryBase<TEntity> : IAmValidatable<TEntity>
    {
        private readonly AbstractValidator<TEntity> _validator;

        protected ValidatableRepositoryBase(AbstractValidator<TEntity> validator)
        {
            _validator = validator;
        }

        public SimpleResult<bool> Validate(TEntity entity)
        {
            var validationResult = _validator.Validate(entity);
            var result = new SimpleResult<bool>(validationResult.IsValid);
            if (!validationResult.IsValid)
            {
                result.AddError(validationResult.Errors.Select(e => new SimpleResultError(e.ErrorMessage, e.ErrorCode)));
            }

            return result;
        }
    }
}