using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RabbitHole;
using Queue = Infrastructure.Configuration.Settings.RabbitMQ.Queue;

namespace Host.Api.Requests.Commands
{
    public class ApiBusBase : BusBase
    {
        private readonly IList<Queue> _queues;
        public IList<Queue> Queues => _queues.ToImmutableList();

        public ApiBusBase(IClient client, IList<Queue> queues) : base(client)
        {
            _queues = queues;
        }

        public Queue ResolveExchange<T>()
            where T: IMessage
        {
            return Queues.First(q => q.Message.Equals(typeof(T).FullName));
        }
    }
}