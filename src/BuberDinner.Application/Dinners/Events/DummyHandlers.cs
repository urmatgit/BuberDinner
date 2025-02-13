using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.MenuAggregate.Events;

using MediatR;

namespace BuberDinner.Application.Dinners.Events
{
    public class DummyHandler : INotificationHandler<MenuCreated>
    {
        public Task Handle(MenuCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
