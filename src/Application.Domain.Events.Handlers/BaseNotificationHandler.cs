using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Domain.Events.Handlers
{
    public abstract class BaseNotificationHandler<T> : INotificationHandler<T> 
        where T : INotification
    {
        public abstract Task Handle(T notification, CancellationToken cancellationToken);
    }
}
