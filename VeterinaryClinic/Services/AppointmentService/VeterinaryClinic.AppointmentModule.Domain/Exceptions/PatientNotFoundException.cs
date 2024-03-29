﻿using System;

namespace VeterinaryClinic.AppointmentModule.Domain.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(string message) : base(message)
        {
        }

        public PatientNotFoundException(int patientId) : base($"No patient with id {patientId} found.")
        {
        }
    }
}
