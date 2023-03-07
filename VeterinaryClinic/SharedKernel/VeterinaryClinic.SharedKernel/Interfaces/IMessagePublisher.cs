namespace VeterinaryClinic.SharedKernel.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(IApplicationEvent applicationEvent);
    }
}
