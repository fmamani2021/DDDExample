namespace VeterinaryClinic.ManagementModule.Domain.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTime TestDate { get; }
    }
}
