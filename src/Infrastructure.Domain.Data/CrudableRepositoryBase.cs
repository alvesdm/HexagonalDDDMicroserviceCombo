using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;

namespace Infrastructure.Domain.Data
{
    public abstract class CrudableRepositoryBase<TEntity, TIndentifier> : ValidatableRepositoryBase<TEntity>, IAmCrudable<TEntity, TIndentifier>
    {
        protected CrudableRepositoryBase(AbstractValidator<TEntity> validator) : base(validator)
        {
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            PerformValidation(entity);
            return await Task.FromResult(entity);
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            PerformValidation(entity);
            return await Task.FromResult(true);
        }

        public abstract Task<bool> Remove(TEntity entity);

        public abstract Task<TEntity> Get(TIndentifier id);

        private void PerformValidation(TEntity entity)
        {
            var result = Validate(entity);
            if (!result.IsSuccess)
            {
                throw new DomainValidationException(entity, result.Errors);
            }
        }
    }
}