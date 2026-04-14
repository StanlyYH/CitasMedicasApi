using CitasMedicasApi.DTOs.Doctors;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllAsync();
        Task<DoctorResponseDto> GetByIdAsync(string id);
        Task<DoctorResponseDto> CreateAsync(CreateDoctorDto dto);
        Task<bool> UpdateAsync(string id, UpdateDoctorDto dto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<DoctorResponseDto>> GetByHealthCenterIdAsync(string healthCenterId);
    }
}