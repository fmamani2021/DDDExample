using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms
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
