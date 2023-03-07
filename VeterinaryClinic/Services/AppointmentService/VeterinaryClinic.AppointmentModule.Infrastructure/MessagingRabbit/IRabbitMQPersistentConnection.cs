using RabbitMQ.Client;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.MessagingRabbit
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
