using CitasMedicasApi.DTOs.HealthCenters;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCentersController : ControllerBase
    {
        private readonly IHealthCenterService _healthCenterService;

        public HealthCentersController(IHealthCenterService healthCenterService)
        {
            _healthCenterService = healthCenterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthCenterResponseDto>>> GetAll()
        {
            var healthCenters = await _healthCenterService.GetAllAsync();
            return Ok(healthCenters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HealthCenterResponseDto>> GetById(string id)
        {
            var healthCenter = await _healthCenterService.GetByIdAsync(id);

            if (healthCenter == null)
            {
                return NotFound();
            }

            return Ok(healthCenter);
        }

        [HttpPost]
        public async Task<ActionResult<HealthCenterResponseDto>> Create(CreateHealthCenterDto dto)
        {
            var createdHealthCenter = await _healthCenterService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdHealthCenter.Id }, createdHealthCenter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateHealthCenterDto dto)
        {
            var updated = await _healthCenterService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _healthCenterService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}