﻿using Ardalis.Specification;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates.Specifications
{
    public class PatientByClientIdSpecification : Specification<Patient>
    {
        public PatientByClientIdSpecification(int clientId)
        {
            Query
                .Where(patient => patient.ClientId == clientId);

            Query.OrderBy(patient => patient.Name);
        }
    }
}
