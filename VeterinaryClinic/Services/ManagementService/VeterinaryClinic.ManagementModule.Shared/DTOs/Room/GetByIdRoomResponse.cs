﻿using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Room
{
    public class GetByIdRoomResponse : BaseResponse
    {
        public RoomDto Room { get; set; } = new RoomDto();

        public GetByIdRoomResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdRoomResponse()
        {
        }
    }
}
