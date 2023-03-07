namespace VeterinaryClinic.SharedKernel.Bus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
