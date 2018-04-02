using Domain.Shared.Results;

namespace Domain.Interfaces
{
    public interface IAmValidatable<TEntity>
    {
        SimpleResult<bool> Validate(TEntity entity);
    }
}