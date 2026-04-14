using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Doctors;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return doctors.Select(DoctorMapper.ToResponseDto);
        }

        public async Task<DoctorResponseDto> GetByIdAsync(string id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return null;
            }

            return DoctorMapper.ToResponseDto(doctor);
        }

        public async Task<DoctorResponseDto> CreateAsync(CreateDoctorDto dto)
        {
            var doctor = DoctorMapper.ToEntity(dto);

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return DoctorMapper.ToResponseDto(doctor);
        }

        public async Task<bool> UpdateAsync(string id, UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return false;
            }

            DoctorMapper.UpdateEntity(doctor, dto);

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                return false;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetByHealthCenterIdAsync(string healthCenterId)
        {
            var doctors = await _context.Doctors
                .Where(d => d.HealthCenterId == healthCenterId)
                .ToListAsync();

            return doctors.Select(DoctorMapper.ToResponseDto);
        }
    }
}