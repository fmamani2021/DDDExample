using AutoMapper;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.AppointmentTypes;

namespace VeterinaryClinic.AppointmentModule.Api.MappingProfile
{
    public class AppointmentTypeProfile : Profile
    {
        public AppointmentTypeProfile()
        {
            CreateMap<AppointmentType, AppointmentTypeDto>()
                .ForMember(dto => dto.AppointmentTypeId, options => options.MapFrom(src => src.Id));
            CreateMap<AppointmentTypeDto, AppointmentType>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.AppointmentTypeId));
        }
    }
}
