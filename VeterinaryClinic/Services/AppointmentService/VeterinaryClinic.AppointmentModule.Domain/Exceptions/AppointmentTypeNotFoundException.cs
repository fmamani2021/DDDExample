using System;

namespace VeterinaryClinic.AppointmentModule.Domain.Exceptions
{
    public class AppointmentTypeNotFoundException : Exception
    {
        public AppointmentTypeNotFoundException(string message) : base(message)
        {
        }

        public AppointmentTypeNotFoundException(int appointmentTypeId) : base($"No appointment type with id {appointmentTypeId} found.")
        {
        }
    }
}
