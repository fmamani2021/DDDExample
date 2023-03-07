using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.ManagementModule.Shared.DTOs.Client;
using VeterinaryClinic.Presentation.ServiceManagement;

namespace VeterinaryClinic.Presentation.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService clientService;
        private readonly PatientService patientService;
        private readonly DoctorService doctorService;

        public ClientController(
            PatientService _patientService, 
            DoctorService _doctorService, 
            ClientService _clientService)
        {
            patientService = _patientService;
            doctorService = _doctorService;
            clientService = _clientService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = await doctorService.ListAsync();
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Read()
        {
            List<ClientDto> clients = await clientService.ListAsync();
            return Json(clients);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] ClientDto model)
        {
            var command = CreateClientRequest.FromDto(model);
            ClientDto client = await clientService.CreateAsync(command);
            return Json(client);
        }


        [HttpPut]
        public async Task<JsonResult> Update([FromBody] ClientDto model)
        {
            var command = UpdateClientRequest.FromDto(model);
            ClientDto client = await clientService.EditAsync(command);
            return Json(client);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] ClientDto model)
        {
            await clientService.DeleteAsync(model.ClientId);
            return Ok();
        }
    }
}
