using CitasMedicasApi.DTOs.HealthCenters;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IHealthCenterService
    {
        Task<IEnumerable<HealthCenterResponseDto>> GetAllAsync();
        Task<HealthCenterResponseDto> GetByIdAsync(string id);
        Task<HealthCenterResponseDto> CreateAsync(CreateHealthCenterDto dto);
        Task<bool> UpdateAsync(string id, UpdateHealthCenterDto dto);
        Task<bool> DeleteAsync(string id);
    }
}