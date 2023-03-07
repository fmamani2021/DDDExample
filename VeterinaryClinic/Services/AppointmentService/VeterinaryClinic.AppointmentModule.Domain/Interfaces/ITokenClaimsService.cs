namespace VeterinaryClinic.AppointmentModule.Domain.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
