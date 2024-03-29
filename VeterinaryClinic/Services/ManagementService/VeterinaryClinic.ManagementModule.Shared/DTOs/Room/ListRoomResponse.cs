﻿using System;
using System.Collections.Generic;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Room
{
    public class ListRoomResponse : BaseResponse
    {
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

        public int Count { get; set; }

        public ListRoomResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListRoomResponse()
        {
        }
    }
}