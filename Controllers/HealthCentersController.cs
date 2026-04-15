using CitasMedicasApi.DTOs.HealthCenters;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [Route("api/healthcenters")]
    [ApiController]
    public class HealthCentersController : ControllerBase
    {
        private readonly IHealthCenterService _healthCenterService;

        public HealthCentersController(IHealthCenterService healthCenterService)
        {
            _healthCenterService = healthCenterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _healthCenterService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneById(string id)
        {
            var response = await _healthCenterService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateHealthCenterDto dto)
        {
            var response = await _healthCenterService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(string id, UpdateHealthCenterDto dto)
        {
            var response = await _healthCenterService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _healthCenterService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}