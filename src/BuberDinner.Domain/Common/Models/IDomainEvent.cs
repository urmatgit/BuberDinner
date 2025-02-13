using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace BuberDinner.Domain.Common.Models
{
    public interface IDomainEvent:INotification
    {
    }
}
