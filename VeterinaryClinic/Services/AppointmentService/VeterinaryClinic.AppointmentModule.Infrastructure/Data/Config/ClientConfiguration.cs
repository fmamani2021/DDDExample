using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data.Config
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients").HasKey(x => x.Id);

            builder.Property(c => c.EmailAddress)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.FullName)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.PreferredName)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.Salutation)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
