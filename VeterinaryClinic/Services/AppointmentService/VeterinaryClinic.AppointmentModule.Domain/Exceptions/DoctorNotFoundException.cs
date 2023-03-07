using System;

namespace VeterinaryClinic.AppointmentModule.Domain.Exceptions
{
    public class DoctorNotFoundException : Exception
    {
        public DoctorNotFoundException(string message) : base(message)
        {
        }

        public DoctorNotFoundException(int doctorId) : base($"No doctor with id {doctorId} found.")
        {
        }
    }
}
