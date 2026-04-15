using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.Doctors;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<ResponseDto<PageDto<List<DoctorResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<DoctorResponseDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<DoctorResponseDto>> CreateAsync(CreateDoctorDto dto);
        Task<ResponseDto<DoctorResponseDto>> EditAsync(string id, UpdateDoctorDto dto);
        Task<ResponseDto<DoctorResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<List<DoctorResponseDto>>> GetByHealthCenterIdAsync(string healthCenterId);
    }
}