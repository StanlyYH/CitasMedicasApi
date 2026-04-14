using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentEntity ToEntity(CreateAppointmentDto dto)
        {
            return new AppointmentEntity
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Reason = dto.Reason,
                Status = dto.Status,
                Notes = dto.Notes
            };
        }

        public static AppointmentResponseDto ToResponseDto(AppointmentEntity entity)
        {
            return new AppointmentResponseDto
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                AppointmentDate = entity.AppointmentDate,
                AppointmentTime = entity.AppointmentTime,
                Reason = entity.Reason,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public static void UpdateEntity(AppointmentEntity entity, UpdateAppointmentDto dto)
        {
            entity.PatientId = dto.PatientId;
            entity.DoctorId = dto.DoctorId;
            entity.AppointmentDate = dto.AppointmentDate;
            entity.AppointmentTime = dto.AppointmentTime;
            entity.Reason = dto.Reason;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}