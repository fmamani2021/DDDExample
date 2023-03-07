using System;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments
{
    public class DeleteAppointmentResponse : BaseResponse
    {

        public ScheduleDto Schedule { get; set; }

        public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteAppointmentResponse()
        {
        }

    }
}
