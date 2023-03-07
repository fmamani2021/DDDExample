using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms
{
    public class CreateRoomResponse : BaseResponse
    {
        public RoomDto Room { get; set; } = new RoomDto();

        public CreateRoomResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateRoomResponse()
        {
        }
    }
}