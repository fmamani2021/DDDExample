using Ardalis.Specification;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Specifications
{
    public class ScheduleClinicIdSpec : Specification<Schedule>
    {
        public ScheduleClinicIdSpec(int clinicId)
        {
            Query
                .Where(schedule =>
                    schedule.ClinicId == clinicId);
        }
    }
}
