using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.AppointmentModule.Shared.DTOs.Appointments;
using VeterinaryClinic.Presentation.ServiceAppointment;

namespace VeterinaryClinic.Presentation.Controllers
{

    public class AppointmentController : Controller
    {        
        private readonly RoomService roomService;
        private readonly ClientService clientService;
        private readonly AppointmentTypeService appointmentTypeService;
        private readonly ScheduleService scheduleService;
        private readonly DoctorService doctorService;
        private readonly AppointmentService appointmentService;
        private readonly IConfiguration configuration;

        public AppointmentController(
            RoomService _roomService,
            ClientService _clientService,
            AppointmentTypeService _appointmentTypeService,
            ScheduleService _scheduleService,
            DoctorService _doctorService,
            AppointmentService _appointmentService,
            IConfiguration _configuration)
        {
            roomService = _roomService;
            clientService = _clientService;
            appointmentTypeService = _appointmentTypeService;
            scheduleService = _scheduleService;
            doctorService = _doctorService;
            appointmentService = _appointmentService;
            configuration = _configuration;
        }

        public async Task<IActionResult> Index()
        {
            var schedule = (await scheduleService.ListAsync()).Single();
            ViewBag.ScheduleId = schedule.Id;                    
            ViewBag.Doctors = await doctorService.ListAsync();
            ViewBag.Clients = await clientService.ListAsync();
            ViewBag.Rooms = await roomService.ListAsync();
            ViewBag.AppointmentTypes = await appointmentTypeService.ListAsync();
            ViewBag.HubAppointment = configuration["SignalR:HubAppointment"];
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> Read(Guid scheduleId)
        {
            List<AppointmentDto> appointments = await appointmentService.ListAsync(scheduleId);
            return Json(appointments);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] AppointmentDto model)
        {
            var command = CreateAppointmentRequest.FromDto(model);
            AppointmentDto appointment = await appointmentService.CreateAsync(command);
            Thread.Sleep(1500);
            return Json(appointment);
        }


        [HttpPut]
        public async Task<JsonResult> Update([FromBody] AppointmentDto model)
        {
            var command = UpdateAppointmentRequest.FromDto(model);
            AppointmentDto appointment = await appointmentService.EditAsync(command);
            return Json(appointment);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] AppointmentDto model)
        {            
            await appointmentService.DeleteAsync(model.ScheduleId, model.AppointmentId);
            return Ok();
        }
    }

}
