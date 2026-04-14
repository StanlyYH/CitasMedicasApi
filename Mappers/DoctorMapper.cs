using CitasMedicasApi.DTOs.Doctors;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorEntity ToEntity(CreateDoctorDto dto)
        {
            return new DoctorEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Specialty = dto.Specialty,
                HealthCenterId = dto.HealthCenterId,
                Status = dto.Status
            };
        }

        public static DoctorResponseDto ToResponseDto(DoctorEntity entity)
        {
            return new DoctorResponseDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Specialty = entity.Specialty,
                HealthCenterId = entity.HealthCenterId,
                Status = entity.Status
            };
        }

        public static void UpdateEntity(DoctorEntity entity, UpdateDoctorDto dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.Specialty = dto.Specialty;
            entity.HealthCenterId = dto.HealthCenterId;
            entity.Status = dto.Status;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}