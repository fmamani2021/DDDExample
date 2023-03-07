using VeterinaryClinic.AppointmentModule.UnitTesting.Builders;
using VeterinaryClinic.SharedKernel.ValueObjects.Custom;
using Xunit;

namespace VeterinaryClinic.AppointmentModule.UnitTesting.Core.ScheduleAggregate.AppointmentTests
{
    public class Appointment_UpdateStartTime
    {
        private readonly DateTimeOffset _startTime = new DateTimeOffset(2021, 01, 01, 10, 00, 00, new TimeSpan(-4, 0, 0));
        private readonly DateTimeOffset _endTime = new DateTimeOffset(2021, 01, 01, 12, 00, 00, new TimeSpan(-4, 0, 0));
        private AppointmentBuilder _builder = new AppointmentBuilder();
        private DateTimeOffsetRange _newDateTimeOffsetRange;

        public Appointment_UpdateStartTime()
        {
            _newDateTimeOffsetRange = DateTimeOffsetRange.Create(_startTime, _endTime);
        }

        [Fact]
        public void UpdatesTimeRange()
        {
            var appointment = _builder
              .WithDefaultValues()
              .WithDateTimeOffsetRange(_newDateTimeOffsetRange)
              .Build();

            var newStartTime = new DateTime(2021, 01, 01, 11, 00, 00);

            appointment.UpdateStartTime(newStartTime, null);

            Assert.Equal(_newDateTimeOffsetRange.DurationInMinutes(), appointment.TimeRange.DurationInMinutes());
            Assert.Equal(newStartTime, appointment.TimeRange.Start);
        }

        [Fact]
        public void CallsHandlerWhenSuccessful()
        {
            var handlerCalled = false;
            Action handler = () => handlerCalled = true;

            var appointment = _builder
              .WithDefaultValues()
              .WithDateTimeOffsetRange(_newDateTimeOffsetRange)
              .Build();

            var newStartTime = new DateTime(2021, 01, 01, 11, 00, 00);

            appointment.UpdateStartTime(newStartTime, handler);

            Assert.True(handlerCalled);
        }

        [Fact]
        public void DoesNotCallHandlerWhenNoActualUpdateMade()
        {
            var handlerCalled = false;
            Action handler = () => handlerCalled = true;

            var appointment = _builder
              .WithDefaultValues()
              .WithDateTimeOffsetRange(_newDateTimeOffsetRange)
              .Build();

            var newStartTime = _startTime;

            appointment.UpdateStartTime(newStartTime, handler);

            Assert.False(handlerCalled);
        }
    }
}
