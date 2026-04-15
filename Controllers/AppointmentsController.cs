using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _appointmentService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(string id)
        {
            var response = await _appointmentService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var response = await _appointmentService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, UpdateAppointmentDto dto)
        {
            var response = await _appointmentService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _appointmentService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(string patientId)
        {
            var response = await _appointmentService.GetByPatientIdAsync(patientId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(string doctorId)
        {
            var response = await _appointmentService.GetByDoctorIdAsync(doctorId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, UpdateAppointmentStatusDto dto)
        {
            var response = await _appointmentService.UpdateStatusAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}