using CitasMedicasApi.Constants;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.Doctors;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _doctorService.GetAllAsync();

            var response = new ResponseDto<IEnumerable<DoctorResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = data
            };

            return StatusCode(HttpStatusCode.OK, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _doctorService.GetByIdAsync(id);

            if (data == null)
            {
                return StatusCode(HttpStatusCode.NOT_FOUND, new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,
                    Data = null
                });
            }

            return StatusCode(HttpStatusCode.OK, new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var data = await _doctorService.CreateAsync(dto);

            return StatusCode(HttpStatusCode.CREATED, new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = data
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateDoctorDto dto)
        {
            var updated = await _doctorService.UpdateAsync(id, dto);

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
            var deleted = await _doctorService.DeleteAsync(id);

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

        [HttpGet("healthcenter/{healthCenterId}")]
        public async Task<IActionResult> GetByHealthCenterId(string healthCenterId)
        {
            var data = await _doctorService.GetByHealthCenterIdAsync(healthCenterId);

            return StatusCode(HttpStatusCode.OK, new ResponseDto<IEnumerable<DoctorResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = data
            });
        }
    }
}