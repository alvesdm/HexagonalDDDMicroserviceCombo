using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Events.Events;
using Domain.Interfaces.Entities;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Infrastructure.Domain.Data
{
    public class ItemRepository : CrudableRepositoryBase<IAmItem, Guid>, IAmItemRepository
    {
        private readonly IMediator _mediator;

        public ItemRepository(
            AbstractValidator<IAmItem> validator,
            IMediator mediator) : base(validator)
        {
            _mediator = mediator;
        }

        public override async Task<IAmItem> Add(IAmItem entity)
        {
            await base.Add(entity);

            entity.UniqueId = Guid.NewGuid();

            await _mediator.Publish(new ItemCreatedEvent(entity));

            return entity;
            //throw new NotImplementedException();
        }

        public override async Task<bool> Update(IAmItem entity)
        {
            await base.Update(entity);
            throw new NotImplementedException();
        }

        public override async Task<bool> Remove(IAmItem entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IAmItem> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<IAmItem>> Find()
        {
            throw new NotImplementedException();
        }
    }
}
