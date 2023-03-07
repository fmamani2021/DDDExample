﻿using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Clients
{
    public class UpdateClientResponse : BaseResponse
    {
        public ClientDto Client { get; set; } = new ClientDto();

        public UpdateClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateClientResponse()
        {
        }
    }
}
