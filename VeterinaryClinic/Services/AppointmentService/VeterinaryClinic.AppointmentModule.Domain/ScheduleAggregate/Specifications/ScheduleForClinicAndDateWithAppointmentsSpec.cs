﻿using System;
using System.Linq;
using Ardalis.Specification;

namespace VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate.Specifications
{
    public class ScheduleForClinicAndDateWithAppointmentsSpec : Specification<Schedule>, ISingleResultSpecification
    {
        public ScheduleForClinicAndDateWithAppointmentsSpec(int clinicId, DateTimeOffset date)
        {
            var endDate = date.AddDays(1);
            Query
                .Where(schedule =>
                  schedule.ClinicId == clinicId &&
                  schedule.Appointments != null)
                .Include(s => s.Appointments.Where(a => a.TimeRange.Start > date && a.TimeRange.End < endDate));
        }
    }
}
