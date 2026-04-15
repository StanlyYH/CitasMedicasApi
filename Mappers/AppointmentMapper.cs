using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public class AppointmentMapper
    {
        public static AppointmentEntity CreateDtoToEntity(CreateAppointmentDto dto)
        {
            return new AppointmentEntity
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Reason = dto.Reason,
                Status = dto.Status,
                Notes = dto.Notes
            };
        }

        public static AppointmentEntity EditDtoToEntity(AppointmentEntity entity, UpdateAppointmentDto dto)
        {
            entity.PatientId = dto.PatientId;
            entity.DoctorId = dto.DoctorId;
            entity.AppointmentDate = dto.AppointmentDate;
            entity.AppointmentTime = dto.AppointmentTime;
            entity.Reason = dto.Reason;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.UpdatedDate = DateTime.UtcNow;

            return entity;
        }

        public static AppointmentResponseDto OneEntityToDto(AppointmentEntity entity)
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

        public static List<AppointmentResponseDto> ListEntityToListDto(List<AppointmentEntity> entities)
        {
            return entities.Select(appointment => new AppointmentResponseDto
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Reason = appointment.Reason,
                Status = appointment.Status,
                Notes = appointment.Notes
            }).ToList();
        }
    }
}