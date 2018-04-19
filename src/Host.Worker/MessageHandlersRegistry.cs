using Application.Hosts.Ports.Commands;
using Autofac;
using Host.Worker.Core;

namespace Host.Worker
{
    internal class MessageHandlersRegistry
    {
        public static void Register(MessageHandlerDictionary dictionary)
        {
            dictionary.Add<AddTaskCommand, AddTaskCommandHandler>();
        }
    }
}