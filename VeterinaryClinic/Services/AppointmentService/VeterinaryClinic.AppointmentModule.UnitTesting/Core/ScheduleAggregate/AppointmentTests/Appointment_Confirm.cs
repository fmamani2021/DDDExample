using AutoFixture;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;
using Xunit;

namespace VeterinaryClinic.AppointmentModule.UnitTesting.Core.ScheduleAggregate.AppointmentTests
{
    public class Appointment_Confirm
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly DateTime _confirmedDate;
        private readonly DateTimeOffsetRange _appointmentRange;

        public Appointment_Confirm()
        {
            _appointmentRange = DateTimeOffsetRange.Create(DateTime.Today.AddHours(12), DateTime.Today.AddHours(13));
            _fixture.Register(() => _appointmentRange);

            _confirmedDate = DateTime.Today.AddHours(16);
        }

        private Appointment GetUnconfirmedAppointment()
        {
            var newAppt = _fixture.Build<Appointment>()
              .Create();
            newAppt.DateTimeConfirmed = null;
            return newAppt;
        }

        [Fact]
        public void ReconfirmResultDateNotChanged()
        {
            var appointment = GetUnconfirmedAppointment();

            appointment.Confirm(_confirmedDate.AddHours(-1));
            appointment.Confirm(_confirmedDate);

            Assert.NotEqual(_confirmedDate, appointment.DateTimeConfirmed);
        }

        [Fact]
        public void ConfirmFirstTimeResultConfirmDateUpdated()
        {
            var appointment = GetUnconfirmedAppointment();

            appointment.Confirm(_confirmedDate);

            Assert.Equal(_confirmedDate, appointment.DateTimeConfirmed);
        }

        [Fact]
        public void ConfirmFirstTimeResultEventCreated()
        {
            var appointment = GetUnconfirmedAppointment();

            appointment.Confirm(_confirmedDate);

            Assert.Contains(appointment.DomainEvents, x => x.GetType() == typeof(AppointmentConfirmedEvent));
        }
    }
}
