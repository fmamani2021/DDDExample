using Ardalis.GuardClauses;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate
{
    public class Appointment : BaseEntity<Guid>
    {
        protected Appointment()
        {
            
        }

        public Appointment(Guid id, int appointmentTypeId, Guid scheduleId, 
            int clientId, int doctorId, int patientId, int roomId, DateTimeOffsetRange 
            timeRange, string title, DateTime? dateTimeConfirmed = null)
        {
            Id = Guard.Against.Default(id, nameof(id));
            AppointmentTypeId = Guard.Against.NegativeOrZero(appointmentTypeId, nameof(appointmentTypeId));
            ScheduleId = Guard.Against.Default(scheduleId, nameof(scheduleId));
            ClientId = Guard.Against.NegativeOrZero(clientId, nameof(clientId));
            DoctorId = Guard.Against.NegativeOrZero(doctorId, nameof(doctorId));
            PatientId = Guard.Against.NegativeOrZero(patientId, nameof(patientId));
            RoomId = Guard.Against.NegativeOrZero(roomId, nameof(roomId));
            TimeRange = Guard.Against.Null(timeRange, nameof(timeRange));
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            DateTimeConfirmed = dateTimeConfirmed;
        }

        public Guid ScheduleId { get; private set; }
        public int ClientId { get; private set; }
        public int PatientId { get; private set; }
        public int RoomId { get; private set; }
        public int DoctorId { get; private set; }
        public int AppointmentTypeId { get; private set; }
        public DateTimeOffsetRange TimeRange { get; private set; }
        public string Title { get; private set; }
        public DateTimeOffset? DateTimeConfirmed { get; set; }
        public bool IsPotentiallyConflicting { get; set; }

        public void UpdateRoom(int newRoomId)
        {
            Guard.Against.NegativeOrZero(newRoomId, nameof(newRoomId));
            if (newRoomId == RoomId) return;

            RoomId = newRoomId;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            AddDomainEvent(appointmentUpdatedEvent);            
        }

        public void UpdateDoctor(int newDoctorId)
        {
            Guard.Against.NegativeOrZero(newDoctorId, nameof(newDoctorId));
            if (newDoctorId == DoctorId) return;

            DoctorId = newDoctorId;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            AddDomainEvent(appointmentUpdatedEvent);
        }

        public void UpdateStartTime(DateTimeOffset newStartTime,
          Action scheduleHandler)
        {
            if (newStartTime == TimeRange.Start) return;

            TimeRange = DateTimeOffsetRange.CreateWithDuration(newStartTime, 
                TimeSpan.FromMinutes(TimeRange.DurationInMinutes()));

            scheduleHandler?.Invoke();

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            AddDomainEvent(appointmentUpdatedEvent);
        }

        public void UpdateTitle(string newTitle)
        {
            if (newTitle == Title) return;

            Title = newTitle;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            AddDomainEvent(appointmentUpdatedEvent);
        }

        public void UpdateAppointmentType(AppointmentType appointmentType,
          Action scheduleHandler)
        {
            Guard.Against.Null(appointmentType, nameof(appointmentType));
            if (AppointmentTypeId == appointmentType.Id) return;

            AppointmentTypeId = appointmentType.Id;
            TimeRange = TimeRange.NewEnd(TimeRange.Start.AddMinutes(appointmentType.Duration));

            scheduleHandler?.Invoke();

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            AddDomainEvent(appointmentUpdatedEvent);
        }

        public void Confirm(DateTimeOffset dateConfirmed)
        {
            if (DateTimeConfirmed.HasValue) return; // no need to reconfirm

            DateTimeConfirmed = dateConfirmed;

            var appointmentConfirmedEvent = new AppointmentConfirmedEvent(this);
            AddDomainEvent(appointmentConfirmedEvent);
        }

        public static Appointment Create(Guid guid, int appointmentTypeId, Guid scheduleId, int clientId, int selectedDoctor, int patientId, int roomId, DateTimeOffsetRange timeRange, string title)
        {
            return new Appointment
            {
                Id = guid,
                AppointmentTypeId = appointmentTypeId,
                ScheduleId = scheduleId,
                ClientId = clientId,
                DoctorId = selectedDoctor,
                PatientId = patientId,
                RoomId = roomId,
                TimeRange = timeRange,
                Title = title
            };
        }

        public static Appointment CreateAlternativo(Guid guid, int appointmentTypeId, Guid scheduleId)
        {
            return new Appointment
            {
                Id = guid,
                AppointmentTypeId = appointmentTypeId,
                ScheduleId = scheduleId
            };
        }

        internal bool ApplyPayVerification(AppointmentType appointmentType)
        {
            throw new NotImplementedException();
        }
    }
}
