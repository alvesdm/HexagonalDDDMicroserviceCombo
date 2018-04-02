using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAmCrudable<TEntity, TIndentifier>
    {
        Task<ServiceResult<TEntity>> Add(TEntity entity);
        Task<ServiceResult<bool>> Update(TEntity entity);
        Task<ServiceResult<bool>> Remove(TEntity entity);
        Task<ServiceResult<TEntity>> Get(TIndentifier id);
    }
}
