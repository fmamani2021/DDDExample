using System;
using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Appointment
{
    public class GetByIdAppointmentRequest : BaseRequest
    {
        public Guid AppointmentId { get; set; }
    }
}
