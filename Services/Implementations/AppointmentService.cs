using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return appointments.Select(AppointmentMapper.ToResponseDto);
        }

        public async Task<AppointmentResponseDto> GetByIdAsync(string id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return null;
            }

            return AppointmentMapper.ToResponseDto(appointment);
        }

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentDto dto)
        {
            var appointment = AppointmentMapper.ToEntity(dto);

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return AppointmentMapper.ToResponseDto(appointment);
        }

        public async Task<bool> UpdateAsync(string id, UpdateAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return false;
            }

            AppointmentMapper.UpdateEntity(appointment, dto);

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetByPatientIdAsync(string patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            return appointments.Select(AppointmentMapper.ToResponseDto);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetByDoctorIdAsync(string doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            return appointments.Select(AppointmentMapper.ToResponseDto);
        }

        public async Task<bool> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return false;
            }

            appointment.Status = dto.Status;
            appointment.UpdatedDate = DateTime.UtcNow;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}