using VeterinaryClinic.ManagementModule.Domain.Interfaces;

namespace VeterinaryClinic.ManagementModule.Api
{
    public class OfficeSettings : IApplicationSettings
    {
        public int ClinicId { get { return 1; } }
        public DateTime TestDate { get { return new DateTime(2030, 9, 23); } }
    }
}
