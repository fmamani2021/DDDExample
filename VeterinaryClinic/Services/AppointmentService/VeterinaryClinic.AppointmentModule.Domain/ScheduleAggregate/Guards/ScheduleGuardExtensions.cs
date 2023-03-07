using Ardalis.GuardClauses;
using VeterinaryClinic.AppointmentModule.Domain.Exceptions;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Guards
{
    public static class ScheduleGuardExtensions
    {
        public static void DuplicateAppointment(this IGuardClause guardClause, IEnumerable<Appointment> existingAppointments, Appointment newAppointment, string parameterName)
        {
            if (existingAppointments.Any(a => a.Id == newAppointment.Id))
            {
                throw new DuplicateAppointmentException("Cannot add duplicate appointment to schedule.", parameterName);
            }
        }
    }
}
