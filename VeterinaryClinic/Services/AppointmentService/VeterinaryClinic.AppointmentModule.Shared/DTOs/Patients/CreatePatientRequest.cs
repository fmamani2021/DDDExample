﻿namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class CreatePatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public string PatientName { get; set; }
    }
}
