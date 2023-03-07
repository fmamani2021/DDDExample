using Ardalis.GuardClauses;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Guards;
using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate
{
    public class Schedule : BaseEntity<Guid>, IAggregateRoot
    {
        public Schedule(Guid id,
          DateTimeOffsetRange dateRange,
          int clinicId)
        {
            Id = Guard.Against.Default(id, nameof(id));
            DateRange = dateRange;
            ClinicId = Guard.Against.NegativeOrZero(clinicId, nameof(clinicId));
        }

        private Schedule(Guid id, int clinicId) // used by EF
        {
            Id = id;
            ClinicId = clinicId;
        }

        public int ClinicId { get; private set; }

        private readonly List<Appointment> _appointments = new List<Appointment>();
        public IEnumerable<Appointment> Appointments => _appointments.AsReadOnly();

        public DateTimeOffsetRange DateRange { get; private set; }

        public Appointment AddNewAppointment(Appointment appointment)
        {
            Guard.Against.Null(appointment, nameof(appointment));
            Guard.Against.Default(appointment.Id, nameof(appointment.Id));
            Guard.Against.DuplicateAppointment(_appointments, appointment, nameof(appointment));

            _appointments.Add(appointment);

            MarkConflictingAppointments();

            var appointmentScheduledEvent = new AppointmentScheduledEvent(appointment);
            AddDomainEvent(appointmentScheduledEvent);

            return appointment;
        }

        public void DeleteAppointment(Appointment appointment)
        {
            Guard.Against.Null(appointment, nameof(appointment));
            var appointmentToDelete = _appointments
                                      .Where(a => a.Id == appointment.Id)
                                      .FirstOrDefault();

            if (appointmentToDelete != null)
            {
                _appointments.Remove(appointmentToDelete);
            }

            MarkConflictingAppointments();

            // TODO: Add appointment deleted event and show delete message in client app
        }

        private void MarkConflictingAppointments()
        {
            foreach (var appointment in _appointments)
            {
                // same patient cannot have two appointments at same time
                var potentiallyConflictingAppointments = _appointments
                    .Where(a => a.PatientId == appointment.PatientId &&
                    a.TimeRange.Overlaps(appointment.TimeRange) &&
                    a != appointment)
                    .ToList();

                // TODO: Add a rule to mark overlapping appointments in same room as conflicting
                // TODO: Add a rule to mark same doctor with overlapping appointments as conflicting

                potentiallyConflictingAppointments.ForEach(a => a.IsPotentiallyConflicting = true);

                appointment.IsPotentiallyConflicting = potentiallyConflictingAppointments.Any();
            }
        }

        public void AppointmentUpdatedHandler()
        {
            // TODO: Add ScheduleHandler calls to UpdateDoctor, UpdateRoom to complete additional rules described in MarkConflictingAppointments
            MarkConflictingAppointments();
        }
    }
}
