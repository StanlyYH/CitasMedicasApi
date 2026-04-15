using CitasMedicasApi.Constants;
using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Appointments;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public AppointmentService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<AppointmentResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<AppointmentEntity> appointmentsQuery = _context.Appointments;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                appointmentsQuery = appointmentsQuery.Where(x =>
                    (x.Reason + " " + x.Status + " " + x.PatientId + " " + x.DoctorId).Contains(searchTerm));
            }

            int totalRows = await appointmentsQuery.CountAsync();

            var appointmentEntities = await appointmentsQuery
                .OrderBy(x => x.AppointmentDate)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var appointmentsDto = AppointmentMapper.ListEntityToListDto(appointmentEntities);

            return new ResponseDto<PageDto<List<AppointmentResponseDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<AppointmentResponseDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = appointmentsDto,
                    HasNextPage = page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<AppointmentResponseDto>> GetOneByIdAsync(string id)
        {
            var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity is null)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false
                };
            }

            return new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = AppointmentMapper.OneEntityToDto(appointmentEntity)
            };
        }

        public async Task<ResponseDto<AppointmentResponseDto>> CreateAsync(CreateAppointmentDto dto)
        {
            bool patientExists = await _context.Patients.AnyAsync(p => p.Id == dto.PatientId);
            if (!patientExists)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Patient not found.",
                    Status = false
                };
            }

            bool doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Doctor not found.",
                    Status = false
                };
            }

            AppointmentEntity appointmentEntity = AppointmentMapper.CreateDtoToEntity(dto);

            _context.Appointments.Add(appointmentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = AppointmentMapper.OneEntityToDto(appointmentEntity)
            };
        }

        public async Task<ResponseDto<AppointmentResponseDto>> EditAsync(string id, UpdateAppointmentDto dto)
        {
            var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity is null)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            bool patientExists = await _context.Patients.AnyAsync(p => p.Id == dto.PatientId);
            if (!patientExists)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Patient not found.",
                    Status = false
                };
            }

            bool doctorExists = await _context.Doctors.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExists)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Doctor not found.",
                    Status = false
                };
            }

            AppointmentMapper.EditDtoToEntity(appointmentEntity, dto);

            _context.Appointments.Update(appointmentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = AppointmentMapper.OneEntityToDto(appointmentEntity)
            };
        }

        public async Task<ResponseDto<AppointmentResponseDto>> DeleteAsync(string id)
        {
            var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity is null)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            _context.Appointments.Remove(appointmentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = AppointmentMapper.OneEntityToDto(appointmentEntity)
            };
        }

        public async Task<ResponseDto<List<AppointmentResponseDto>>> GetByPatientIdAsync(string patientId)
        {
            var appointmentEntities = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            var appointmentsDto = AppointmentMapper.ListEntityToListDto(appointmentEntities);

            return new ResponseDto<List<AppointmentResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = appointmentsDto
            };
        }

        public async Task<ResponseDto<List<AppointmentResponseDto>>> GetByDoctorIdAsync(string doctorId)
        {
            var appointmentEntities = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            var appointmentsDto = AppointmentMapper.ListEntityToListDto(appointmentEntities);

            return new ResponseDto<List<AppointmentResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = appointmentsDto
            };
        }

        public async Task<ResponseDto<AppointmentResponseDto>> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto)
        {
            var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity is null)
            {
                return new ResponseDto<AppointmentResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            appointmentEntity.Status = dto.Status;
            appointmentEntity.UpdatedDate = DateTime.UtcNow;

            _context.Appointments.Update(appointmentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<AppointmentResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = AppointmentMapper.OneEntityToDto(appointmentEntity)
            };
        }
    }
}