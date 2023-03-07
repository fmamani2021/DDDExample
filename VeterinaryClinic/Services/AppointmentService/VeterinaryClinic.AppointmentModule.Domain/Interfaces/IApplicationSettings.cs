namespace VeterinaryClinic.AppointmentModule.Domain.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTimeOffset TestDate { get; }
    }
}
