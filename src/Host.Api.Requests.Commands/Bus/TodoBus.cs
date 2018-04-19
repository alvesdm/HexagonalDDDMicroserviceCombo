using System.Collections.Generic;
using Application.Hosts.Ports.Commands;
using RabbitHole;
using Queue = Infrastructure.Configuration.Settings.RabbitMQ.Queue;

namespace Host.Api.Requests.Commands.Bus
{
    public class TodoBus : ApiBusBase, IAmTodoBus
    {
        public TodoBus(IClient client, IList<Queue> queues) : base(client, queues)
        {
        }

        public void Publish(AddTaskCommand message)
        {
            var exchange = ResolveExchange<AddTaskCommand>().Exchange;

            Client
                .DeclareExchange(e => e.WithName(exchange))
                .Publish<AddTaskCommand>(
                    m =>
                        m.WithExchange(exchange)
                            .WithMessage(message));
        }
    }
}