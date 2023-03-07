using System;

namespace VeterinaryClinic.AppointmentModule.Domain.Exceptions
{
    public class DuplicateAppointmentException : ArgumentException
    {
        public DuplicateAppointmentException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
