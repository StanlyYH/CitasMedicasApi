using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.HealthCenters;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class HealthCenterService : IHealthCenterService
    {
        private readonly ApplicationDbContext _context;

        public HealthCenterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HealthCenterResponseDto>> GetAllAsync()
        {
            var healthCenters = await _context.HealthCenters
                .OrderBy(h => h.Name)
                .ToListAsync();

            return healthCenters.Select(HealthCenterMapper.ToResponseDto);
        }

        public async Task<HealthCenterResponseDto> GetByIdAsync(string id)
        {
            var healthCenter = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenter == null)
            {
                return null;
            }

            return HealthCenterMapper.ToResponseDto(healthCenter);
        }

        public async Task<HealthCenterResponseDto> CreateAsync(CreateHealthCenterDto dto)
        {
            var healthCenter = HealthCenterMapper.ToEntity(dto);

            _context.HealthCenters.Add(healthCenter);
            await _context.SaveChangesAsync();

            return HealthCenterMapper.ToResponseDto(healthCenter);
        }

        public async Task<bool> UpdateAsync(string id, UpdateHealthCenterDto dto)
        {
            var healthCenter = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenter == null)
            {
                return false;
            }

            HealthCenterMapper.UpdateEntity(healthCenter, dto);

            _context.HealthCenters.Update(healthCenter);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var healthCenter = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenter == null)
            {
                return false;
            }

            _context.HealthCenters.Remove(healthCenter);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}