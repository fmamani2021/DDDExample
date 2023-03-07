using System;

namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms
{
    public class DeleteRoomResponse : BaseResponse
    {
        public DeleteRoomResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteRoomResponse()
        {
        }
    }
}