namespace VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms
{
    public class UpdateRoomRequest : BaseRequest
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
    }
}