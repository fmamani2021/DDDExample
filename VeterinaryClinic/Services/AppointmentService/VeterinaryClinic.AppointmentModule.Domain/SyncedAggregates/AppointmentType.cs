﻿using VeterinaryClinic.SharedKernel;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates
{
    public class AppointmentType : BaseEntity<int>, IAggregateRoot
    {
        public AppointmentType(int id, string name, string code, int duration)
        {
            Id = id;
            Name = name;
            Code = code;
            Duration = duration;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public int Duration { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
