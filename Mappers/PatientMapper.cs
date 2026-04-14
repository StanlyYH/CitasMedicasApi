using System.ComponentModel.DataAnnotations;
using CitasMedicasApi.Dtos.Patients;
using CitasMedicasApi.DTOs.Patients;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public class PatientMapper
    {
        public static PatientEntity CreateDtoToEntity(CreatePatientDto dto)
        {
            return new PatientEntity
            {
                Id = Guid.NewGuid().ToString(),
                DNI = dto.DNI,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Phone = dto.Phone,
                Address = dto.Address
            };
        }

        public static PatientEntity EditDtoToEntity(PatientEntity entity, UpdatePatientDto dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.DNI = dto.DNI;
            entity.BirthDate = dto.BirthDate;
            entity.Gender = dto.Gender;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;
            return entity;
        }

        public static List<PatientDto> ListEntityToListDto(List<PatientEntity> entities)
        {
            return entities.Select(person => new PatientDto
            {
                Id = person.Id,
                DNI = person.DNI,
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Phone = person.Phone,
                Address = person.Address,
            }).ToList();
        }
    }
}