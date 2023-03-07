using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Room
{
    public class GetByIdRoomRequest : BaseRequest
    {
        public int RoomId { get; set; }
    }
}
