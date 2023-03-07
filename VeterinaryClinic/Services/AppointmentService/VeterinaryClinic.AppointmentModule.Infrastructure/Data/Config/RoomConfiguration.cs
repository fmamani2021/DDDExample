using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data.Config
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms").HasKey(x => x.Id);

            builder.Property(r => r.Name)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
