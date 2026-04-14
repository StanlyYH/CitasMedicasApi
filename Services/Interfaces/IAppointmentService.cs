using CitasMedicasApi.DTOs.Appointments;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAllAsync();
        Task<AppointmentResponseDto> GetByIdAsync(string id);
        Task<AppointmentResponseDto> CreateAsync(CreateAppointmentDto dto);
        Task<bool> UpdateAsync(string id, UpdateAppointmentDto dto);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<AppointmentResponseDto>> GetByPatientIdAsync(string patientId);
        Task<IEnumerable<AppointmentResponseDto>> GetByDoctorIdAsync(string doctorId);
        Task<bool> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto);
    }
}