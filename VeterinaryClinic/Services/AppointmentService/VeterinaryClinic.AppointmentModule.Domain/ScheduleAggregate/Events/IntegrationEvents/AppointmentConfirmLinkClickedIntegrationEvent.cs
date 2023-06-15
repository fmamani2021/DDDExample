﻿using System;
using VeterinaryClinic.SharedKernel;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Events.IntegrationEvents
{
    // This is fired by the message queue handler when an appointment should
    // be marked confirmed. It happens before the appointment is confirmed in
    // the model.
    public class AppointmentConfirmLinkClickedIntegrationEvent : BaseIntegrationEvent
    {
        public AppointmentConfirmLinkClickedIntegrationEvent() : this(DateTimeOffset.Now)
        {
        }

        public AppointmentConfirmLinkClickedIntegrationEvent(DateTimeOffset dateOccurred)
        {
            DateOccurred = dateOccurred;
        }

        public Guid AppointmentId { get; set; }
        public string EventType
        {
            get
            {
                return nameof(AppointmentConfirmLinkClickedIntegrationEvent);
            }
        }
    }
}
