using RabbitMQ.Client;

namespace VeterinaryClinic.ManagementModule.Infrastructure.MessagingRabbit
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
