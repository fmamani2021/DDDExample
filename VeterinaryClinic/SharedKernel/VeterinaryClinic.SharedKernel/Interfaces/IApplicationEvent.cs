namespace VeterinaryClinic.SharedKernel.Interfaces
{
    public interface IApplicationEvent
    {
        string EventType { get; }
    }
}
