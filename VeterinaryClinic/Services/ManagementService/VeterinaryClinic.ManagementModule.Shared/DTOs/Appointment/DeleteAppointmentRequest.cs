using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class DeleteAppointmentRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
