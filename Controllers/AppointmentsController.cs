using CitasMedicasApi.Constants;
using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _appointmentService.GetAllAsync();

            return StatusCode(HttpStatusCode.OK, new ResponseDto<IEnumerable<AppointmentResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _appointmentService.GetByIdAsync(id);

            if (data == null)
            {
                return StatusCode(HttpStatusCode.NOT_FOUND, new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                    Data = null
                });
            }

            return StatusCode(HttpStatusCode.OK, new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var data = await _appointmentService.CreateAsync(dto);

            return StatusCode(HttpStatusCode.CREATED, new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = data
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAppointmentDto dto)
        {
            var updated = await _appointmentService.UpdateAsync(id, dto);

            if (!updated)
            {
                return StatusCode(HttpStatusCode.NOT_FOUND, new ResponseDto<object>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                    Data = null
                });
            }

            return StatusCode(HttpStatusCode.OK, new ResponseDto<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Status = true,
                Data = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _appointmentService.DeleteAsync(id);

            if (!deleted)
            {
                return StatusCode(HttpStatusCode.NOT_FOUND, new ResponseDto<object>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                    Data = null
                });
            }

            return StatusCode(HttpStatusCode.OK, new ResponseDto<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Status = true,
                Data = null
            });
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId(string patientId)
        {
            var data = await _appointmentService.GetByPatientIdAsync(patientId);

            return StatusCode(HttpStatusCode.OK, new ResponseDto<IEnumerable<AppointmentResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = data
            });
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId(string doctorId)
        {
            var data = await _appointmentService.GetByDoctorIdAsync(doctorId);

            return StatusCode(HttpStatusCode.OK, new ResponseDto<IEnumerable<AppointmentResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = data
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateAppointmentStatusDto dto)
        {
            var updated = await _appointmentService.UpdateStatusAsync(id, dto);

            if (!updated)
            {
                return StatusCode(HttpStatusCode.NOT_FOUND, new ResponseDto<object>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                    Data = null
                });
            }

            return StatusCode(HttpStatusCode.OK, new ResponseDto<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Status = true,
                Data = null
            });
        }
    }
}