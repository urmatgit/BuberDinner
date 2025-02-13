using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Common.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuberDinner.Infrastructure.Persistence.Interceptors
{
    public class PublishDomainEventsInterceptor: SaveChangesInterceptor
    {
        private readonly IPublisher _mediator;
        public PublishDomainEventsInterceptor(IPublisher publisher)
        {
                _mediator = publisher;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PusblishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public  override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await PusblishDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private  async Task PusblishDomainEvents(DbContext? dbcontext)
        {
            if (dbcontext is null)
                return;
            var entitiesWithDomainEvents = dbcontext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entity=>entity.Entity.DomainEvents.Any())
                .Select(entity=>entity.Entity)
                .ToList();
            var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents).ToList();
            entitiesWithDomainEvents.ForEach(entity =>entity.ClearDomainEvents());
            foreach(var  domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
