using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.HealthCenters;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IHealthCenterService
    {
        Task<ResponseDto<PageDto<List<HealthCenterResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<HealthCenterResponseDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<HealthCenterResponseDto>> CreateAsync(CreateHealthCenterDto dto);
        Task<ResponseDto<HealthCenterResponseDto>> EditAsync(string id, UpdateHealthCenterDto dto);
        Task<ResponseDto<HealthCenterResponseDto>> DeleteAsync(string id);
    }
}