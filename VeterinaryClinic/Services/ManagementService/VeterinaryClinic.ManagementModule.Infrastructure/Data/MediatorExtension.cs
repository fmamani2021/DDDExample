using MediatR;
using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.ManagementModule.Infrastructure.Data
{
    internal static class MediatorExtension
    {
        public static async Task DispatchEventsAsync<T>(this IMediator mediator, DbContextBase<T> context) where T : DbContextBase<T>
        {
            if (mediator == null) return;

            var entries = context.ChangeTracker.Entries();

            var entitiesWithEventsInt = context.ChangeTracker
              .Entries()
              .Select(e => e.Entity as BaseEntity<int>)
              .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
              .ToList();

            var entitiesWithEventsGuid = context.ChangeTracker
              .Entries()
              .Select(e => e.Entity as BaseEntity<Guid>)
              .Where(e => e?.DomainEvents != null && e.DomainEvents.Any())
              .ToList();

            List<BaseDomainEvent> domainEvents = GetDomainEvents(entitiesWithEventsInt, entitiesWithEventsGuid);

            await mediator.DispatchDomainEventsAsync(domainEvents);
            DispatchIntegrationEventsAsync(domainEvents, context);

            ClearDomainEvents(entitiesWithEventsInt);
            ClearDomainEvents(entitiesWithEventsGuid);

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
            if (context.eventMapper == null || context.eventBus == null || domainEvents.Any() == false) return;
            
            var integrationEvents = context.eventMapper.Map(domainEvents);
            if (integrationEvents != null)
            {
                integrationEvents.ForEach(integrationEvent =>
                    context.eventBus.Publish(integrationEvent)
                );                
            }
        }

        private static void ClearDomainEvents<E>(List<BaseEntity<E>> entitiesWithEvents) where E : struct
        {
            if (entitiesWithEvents == null) return;
            entitiesWithEvents.ForEach(entity => entity.ClearDomainEvents());

            //foreach (var entity in entitiesWithEvents)
            //{
            //    entity.DomainEvents.Clear();
            //}
        }

        private static List<BaseDomainEvent> GetDomainEvents(
            List<BaseEntity<int>?> entitiesWithEventsInt, 
            List<BaseEntity<Guid>?> entitiesWithEventsGuid)
        {
            var domainEvents = new List<BaseDomainEvent>();

            var domainEventsInt = entitiesWithEventsInt
               .SelectMany(x => x.DomainEvents)
               .ToList();

            if (domainEventsInt != null && domainEventsInt.Any())
            {
                domainEvents.AddRange(domainEventsInt);
            }

            var domainEventsGuid = entitiesWithEventsGuid
               .SelectMany(x => x.DomainEvents)
               .ToList();

            if (domainEventsGuid != null && domainEventsGuid.Any())
            {
                domainEvents.AddRange(domainEventsGuid);
            }

            return domainEvents;
        }
    }
}
