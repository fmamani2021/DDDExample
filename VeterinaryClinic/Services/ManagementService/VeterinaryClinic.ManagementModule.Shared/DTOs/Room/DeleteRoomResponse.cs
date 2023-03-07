using System;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Room
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