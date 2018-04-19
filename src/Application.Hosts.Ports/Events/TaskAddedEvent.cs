using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Hosts.Ports.Events
{
    public class TaskAddedEvent
    {
        public string Description { get; }

        public TaskAddedEvent(string description)
        {
            Description = description;
        }
    }
}
