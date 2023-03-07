using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data.Config
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Ignore(s => s.DateRange);
        }
    }
}
