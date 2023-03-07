using System;

namespace VeterinaryClinic.AppointmentModule.Domain.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string message) : base(message)
        {
        }

        public ClientNotFoundException(int clientId) : base($"No client with id {clientId} found.")
        {
        }
    }
}
