using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IAmItemRepository : IAmCrudable<IAmItem, Guid>
    {
        Task<IQueryable<IAmItem>> Find();
    }
}
