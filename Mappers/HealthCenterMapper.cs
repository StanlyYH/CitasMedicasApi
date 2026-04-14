using CitasMedicasApi.DTOs.HealthCenters;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Mappers
{
    public static class HealthCenterMapper
    {
        public static HealthCenterEntity ToEntity(CreateHealthCenterDto dto)
        {
            return new HealthCenterEntity
            {
                Name = dto.Name,
                Address = dto.Address,
                Municipality = dto.Municipality,
                Department = dto.Department,
                PhoneNumber = dto.PhoneNumber,
                Status = dto.Status
            };
        }

        public static HealthCenterResponseDto ToResponseDto(HealthCenterEntity entity)
        {
            return new HealthCenterResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Municipality = entity.Municipality,
                Department = entity.Department,
                PhoneNumber = entity.PhoneNumber,
                Status = entity.Status,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static void UpdateEntity(HealthCenterEntity entity, UpdateHealthCenterDto dto)
        {
            entity.Name = dto.Name;
            entity.Address = dto.Address;
            entity.Municipality = dto.Municipality;
            entity.Department = dto.Department;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Status = dto.Status;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}