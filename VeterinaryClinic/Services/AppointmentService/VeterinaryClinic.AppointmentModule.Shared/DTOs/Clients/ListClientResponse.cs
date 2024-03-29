﻿using System;
using System.Collections.Generic;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
{
    public class ListClientResponse : BaseResponse
    {
        public List<ClientDto> Clients { get; set; } = new List<ClientDto>();

        public int Count { get; set; }

        public ListClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListClientResponse()
        {
        }
    }
}
