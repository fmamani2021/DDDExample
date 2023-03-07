namespace VeterinaryClinic.ManagementModule.Shared.DTOs.Room
{
    public class UpdateRoomRequest : BaseRequest
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
    }
}