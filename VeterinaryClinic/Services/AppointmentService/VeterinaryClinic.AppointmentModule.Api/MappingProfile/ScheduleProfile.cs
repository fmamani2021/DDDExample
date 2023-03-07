using AutoMapper;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Schedules;

namespace VeterinaryClinic.AppointmentModule.Api.MappingProfile
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>()
              .ForPath(dto => dto.AppointmentIds, options => options.MapFrom(src => src.Appointments.Select(x => x.Id)));
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<CreateScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
            CreateMap<DeleteScheduleRequest, Schedule>();
        }
    }
}
