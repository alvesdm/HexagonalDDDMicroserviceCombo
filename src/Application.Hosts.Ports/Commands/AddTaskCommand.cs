using System;
using System.Collections.Generic;
using System.Text;
using RabbitHole;

namespace Application.Hosts.Ports.Commands
{
    public class AddTaskCommand : ICommand
    {
        public string Description { get; }

        public AddTaskCommand(string description)
        {
            Description = description;
        }
    }
}
