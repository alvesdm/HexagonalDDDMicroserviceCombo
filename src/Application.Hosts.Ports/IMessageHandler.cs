using System.Threading.Tasks;
using RabbitHole;

namespace Application.Hosts.Ports
{
    public interface IMessageHandler<TMessage>
        where TMessage : IMessage
    {
        Task Handle(TMessage message);
    }
}