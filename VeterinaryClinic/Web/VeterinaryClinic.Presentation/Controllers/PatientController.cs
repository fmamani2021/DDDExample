using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Presentation.ServiceAppointment;

namespace VeterinaryClinic.Presentation.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService patientService;

        public PatientController(
            PatientService _patientService)
        {
            patientService = _patientService;
        }

        [HttpGet]
        public async Task<JsonResult> ListByClientId(int clientId)
        {
            return Json(await patientService.ListAsync(clientId));
        }
    }
}
