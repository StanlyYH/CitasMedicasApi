using CitasMedicasApi.DTOs.Doctors;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public class DoctorMapper
    {
        public static DoctorEntity CreateDtoToEntity(CreateDoctorDto dto)
        {
            return new DoctorEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Specialty = dto.Specialty,
                HealthCenterId = dto.HealthCenterId,
                Status = dto.Status
            };
        }

        public static DoctorEntity EditDtoToEntity(DoctorEntity entity, UpdateDoctorDto dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.Specialty = dto.Specialty;
            entity.HealthCenterId = dto.HealthCenterId;
            entity.Status = dto.Status;
            entity.UpdatedDate = DateTime.UtcNow;

            return entity;
        }

        public static DoctorResponseDto OneEntityToDto(DoctorEntity entity)
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

        public static List<DoctorResponseDto> ListEntityToListDto(List<DoctorEntity> entities)
        {
            return entities.Select(doctor => new DoctorResponseDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Specialty = doctor.Specialty,
                HealthCenterId = doctor.HealthCenterId,
                Status = doctor.Status
            }).ToList();
        }
    }
}