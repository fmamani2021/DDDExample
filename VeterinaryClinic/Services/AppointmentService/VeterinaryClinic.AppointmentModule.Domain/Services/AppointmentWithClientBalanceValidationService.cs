using VeterinaryClinic.AppointmentModule.Domain.Interfaces;
using VeterinaryClinic.AppointmentModule.Domain.ScheduleAggregate;
using VeterinaryClinic.AppointmentModule.Domain.SyncedAggregates;

namespace VeterinaryClinic.AppointmentModule.Domain.Services
{
    public sealed class AppointmentWithClientBalanceValidationService
    {
        public async Task AddNewAppointment(
            Schedule schedule, 
            Appointment appoinment,
            AppointmentType appointmentType, 
            IExternalService externalService
        ) 
        {
            //Examen de diagnóstico 
            if (appoinment.ApplyPayVerification(appointmentType)) 
            {
                //Verificar el saldo del cliente
                var clientBalance = await externalService.VerifyClientBalance(appoinment.ClientId); 
                if (clientBalance > 0)
                {
                    schedule.AddNewAppointment(appoinment);
                } 
                else {
                    //Lanzar excepcion de saldo insuficiente
                }
            } else { 
                schedule.AddNewAppointment(appoinment);
            }
        }
    }
}
