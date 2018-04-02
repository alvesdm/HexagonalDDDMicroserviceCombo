using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAmCrudable<TEntity, TIndentifier> : IAmValidatable<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(TEntity entity);
        Task<TEntity> Get(TIndentifier id);
    }
}
