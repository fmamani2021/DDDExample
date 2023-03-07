using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
{
    public class ListAppointmentRequest : BaseRequest
    {
        public const string Route = "api/schedule/{ScheduleId}/appointments";
        public Guid ScheduleId { get; set; }
    }
}
