using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.Events
{
    public record MenuCreated(Menu menu): IDomainEvent
    {
    }
}
