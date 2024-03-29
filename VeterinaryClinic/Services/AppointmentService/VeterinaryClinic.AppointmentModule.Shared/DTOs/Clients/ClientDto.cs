﻿namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredDoctorId { get; set; }
        public IList<int> Patients { get; set; } = new List<int>();
    }
}
