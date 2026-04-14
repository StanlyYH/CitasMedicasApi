using CitasMedicasApi.Dtos.Patients;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.Patients;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IPatientService
    {
        Task<ResponseDto<PageDto<List<PatientDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<PatientDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<PatientResponseDto>> CreateAsync(CreatePatientDto dto);
        Task<ResponseDto<PatientResponseDto>> EditAsync(string id, UpdatePatientDto dto);
        Task<ResponseDto<PatientResponseDto>> DeleteAsync(string id);
    }
}