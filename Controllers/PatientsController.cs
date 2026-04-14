using CitasMedicasApi.Dtos.Patients;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CitasMedicasApi.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _patientService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _patientService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result); 
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePatientDto dto)
        {
            
            var result = await _patientService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdatePatientDto dto)
        {
            var result = await  _patientService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }
    
         [HttpDelete("{id}")]
         public async Task<ActionResult> Delete(string id)
        {
            
            var result = await _patientService.DeleteAsync(id    );
            return StatusCode(result.StatusCode, result);
        }

    }
}