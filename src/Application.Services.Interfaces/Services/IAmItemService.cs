using System;
using System.Threading.Tasks;
using Domain.Interfaces.Entities;

namespace Application.Services.Interfaces.Services
{
    public interface IAmItemService : IAmCrudable<IAmItem, Guid>
    {
        Task<ServiceResult<QueryResult<IAmItem>>> Find(string criteria, int page = 1, int limit = 100);
    }
}
