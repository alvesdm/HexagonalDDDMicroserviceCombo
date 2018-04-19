namespace Application.Hosts.Ports
{
    public interface ICommandHandler<TCommand, TReturn> : IMessageHandler<TCommand>
        where TCommand : ICommand
    {
    }
}