namespace VeterinaryClinic.SharedKernel
{
    /// <summary>
    /// Base types for all Entities which track state using a given Id.
    /// </summary>
    public abstract class BaseEntity<TId>
    {
        public BaseEntity()
        {
            _domainEvents = new List<BaseDomainEvent>();
        }

        public TId Id { get; set; } = default!;

        private List<BaseDomainEvent> _domainEvents;
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(BaseDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
