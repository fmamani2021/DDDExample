﻿namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Patients
{
    public class UpdatePatientRequest : BaseRequest
    {
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
    }
}
