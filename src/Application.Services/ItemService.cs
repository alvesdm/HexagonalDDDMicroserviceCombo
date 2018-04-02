using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Services;
using Domain.Interfaces.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class ItemService : IAmItemService
    {
        private readonly IAmItemRepository _itemRepository;

        public ItemService(IAmItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ServiceResult<IAmItem>> Add(IAmItem entity)
        {
            var result = await _itemRepository.Add(entity);

            return new ServiceResult<IAmItem>(result);
        }

        public async Task<ServiceResult<bool>> Update(IAmItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> Remove(IAmItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IAmItem>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<QueryResult<IAmItem>>> Find(string criteria, int page = 1, int limit = 100)
        {
            var result = (await _itemRepository.Find())
                .Where(x => x.Description.Contains(criteria));

            var offset = (page - 1) * limit;
            return new ServiceResult<QueryResult<IAmItem>>(new QueryResult<IAmItem>(result, offset, limit));
        }
    }
}
