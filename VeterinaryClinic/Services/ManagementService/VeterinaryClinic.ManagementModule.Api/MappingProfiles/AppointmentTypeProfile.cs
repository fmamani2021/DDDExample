using AutoMapper;
using VeterinaryClinic.ManagementModule.Domain.Aggregates;
using VeterinaryClinic.ManagementModule.Shared.DTOs.AppointmentType;

namespace VeterinaryClinic.ManagementModule.Api.MappingProfiles
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
