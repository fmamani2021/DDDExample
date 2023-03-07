using AutoMapper;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Doctors;

namespace VeterinaryClinic.AppointmentModule.Api.MappingProfile
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dto => dto.DoctorId, options => options.MapFrom(src => src.Id));
            CreateMap<DoctorDto, Doctor>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.DoctorId));
            CreateMap<CreateDoctorRequest, Doctor>();
            CreateMap<UpdateDoctorRequest, Doctor>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.DoctorId));
            CreateMap<DeleteDoctorRequest, Doctor>();
        }
    }
}
