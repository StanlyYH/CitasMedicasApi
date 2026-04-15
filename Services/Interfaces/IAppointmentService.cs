using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.DTOs.Common;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ResponseDto<PageDto<List<AppointmentResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<AppointmentResponseDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<AppointmentResponseDto>> CreateAsync(CreateAppointmentDto dto);
        Task<ResponseDto<AppointmentResponseDto>> EditAsync(string id, UpdateAppointmentDto dto);
        Task<ResponseDto<AppointmentResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<List<AppointmentResponseDto>>> GetByPatientIdAsync(string patientId);
        Task<ResponseDto<List<AppointmentResponseDto>>> GetByDoctorIdAsync(string doctorId);
        Task<ResponseDto<AppointmentResponseDto>> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto);
    }
}