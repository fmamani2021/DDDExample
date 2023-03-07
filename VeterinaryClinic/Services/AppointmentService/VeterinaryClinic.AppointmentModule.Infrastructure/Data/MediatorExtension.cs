using Ardalis.Specification;
using MediatR;
using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data
{
    internal static class MediatorExtension
    {
        public static async Task DispatchEventsAsync<T>(this IMediator mediator, DbContextBase<T> context) where T : DbContextBase<T>
        {
            if (mediator == null) return;

            var entitiesWithEvents = context.ChangeTracker
                .Entries()
                .Select(e => e.Entity as BaseEntity<Guid>)
                .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
            .ToList();

            var domainEvents = entitiesWithEvents
               .SelectMany(x => x.DomainEvents)
               .ToList();

            await mediator.DispatchDomainEventsAsync(domainEvents);
            DispatchIntegrationEventsAsync(domainEvents, context);

            ClearDomainEvents(entitiesWithEvents);

        }

        private static async Task DispatchDomainEventsAsync(this IMediator mediator, List<BaseDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }

        private static void DispatchIntegrationEventsAsync<T>(IEnumerable<BaseDomainEvent> domainEvents,
            DbContextBase<T> context) where T : DbContextBase<T>
        {
            if (context.EventMapper == null || context.eventBus == null) return;

            var integrationEvents = context.EventMapper.Map(domainEvents);
            if (integrationEvents != null)
            {
                integrationEvents.ForEach(integrationEvent =>
                    context.eventBus.Publish(integrationEvent)
                );
            }
        }

        private static void ClearDomainEvents(List<BaseEntity<Guid>> entitiesWithEvents)
        {
            if (entitiesWithEvents == null) return;
            entitiesWithEvents.ForEach(entity => entity.ClearDomainEvents());
        }
    }
}
