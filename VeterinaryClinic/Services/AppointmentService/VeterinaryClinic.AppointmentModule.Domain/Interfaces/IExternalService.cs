namespace VeterinaryClinic.AppointmentModule.Domain.Interfaces
{
    public interface IExternalService
    {
        Task<decimal> VerifyClientBalance(int clientId);
    }
}
