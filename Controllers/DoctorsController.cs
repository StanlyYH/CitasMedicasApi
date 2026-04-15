using CitasMedicasApi.DTOs.Doctors;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _doctorService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(string id)
        {
            var response = await _doctorService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorDto dto)
        {
            var response = await _doctorService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, UpdateDoctorDto dto)
        {
            var response = await _doctorService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _doctorService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("healthcenter/{healthCenterId}")]
        public async Task<IActionResult> GetByHealthCenterId(string healthCenterId)
        {
            var response = await _doctorService.GetByHealthCenterIdAsync(healthCenterId);
            return StatusCode(response.StatusCode, response);
        }
    }
}