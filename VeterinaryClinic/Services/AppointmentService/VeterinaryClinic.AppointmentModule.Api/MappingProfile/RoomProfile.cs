using AutoMapper;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Rooms;

namespace VeterinaryClinic.AppointmentModule.Api.MappingProfile
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(dto => dto.RoomId, options => options.MapFrom(src => src.Id));
            CreateMap<RoomDto, Room>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.RoomId));
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.RoomId));
            CreateMap<DeleteRoomRequest, Room>();
        }
    }
}
