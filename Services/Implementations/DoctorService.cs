using CitasMedicasApi.Constants;
using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Common;
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
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public DoctorService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<DoctorResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<DoctorEntity> doctorsQuery = _context.Doctors;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                doctorsQuery = doctorsQuery.Where(x =>
                    (x.FirstName + " " + x.LastName + " " + x.Specialty + " " + x.Email).Contains(searchTerm));
            }

            int totalRows = await doctorsQuery.CountAsync();

            var doctorEntities = await doctorsQuery
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var doctorsDto = DoctorMapper.ListEntityToListDto(doctorEntities);

            return new ResponseDto<PageDto<List<DoctorResponseDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<DoctorResponseDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = doctorsDto,
                    HasNextPage = page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<DoctorResponseDto>> GetOneByIdAsync(string id)
        {
            var doctorEntity = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctorEntity is null)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false
                };
            }

            return new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = DoctorMapper.OneEntityToDto(doctorEntity)
            };
        }

        public async Task<ResponseDto<DoctorResponseDto>> CreateAsync(CreateDoctorDto dto)
        {
            bool healthCenterExists = await _context.HealthCenters.AnyAsync(h => h.Id == dto.HealthCenterId);
            if (!healthCenterExists)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Health center not found.",
                    Status = false
                };
            }

            bool doctorExists = await _context.Doctors.AnyAsync(d => d.Email == dto.Email);
            if (doctorExists)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Message = "A doctor with the same email already exists.",
                    Status = false
                };
            }

            DoctorEntity doctorEntity = DoctorMapper.CreateDtoToEntity(dto);

            _context.Doctors.Add(doctorEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = DoctorMapper.OneEntityToDto(doctorEntity)
            };
        }

        public async Task<ResponseDto<DoctorResponseDto>> EditAsync(string id, UpdateDoctorDto dto)
        {
            var doctorEntity = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctorEntity is null)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            bool healthCenterExists = await _context.HealthCenters.AnyAsync(h => h.Id == dto.HealthCenterId);
            if (!healthCenterExists)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = "Health center not found.",
                    Status = false
                };
            }

            DoctorMapper.EditDtoToEntity(doctorEntity, dto);

            _context.Doctors.Update(doctorEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = DoctorMapper.OneEntityToDto(doctorEntity)
            };
        }

        public async Task<ResponseDto<DoctorResponseDto>> DeleteAsync(string id)
        {
            var doctorEntity = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctorEntity is null)
            {
                return new ResponseDto<DoctorResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            _context.Doctors.Remove(doctorEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<DoctorResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = DoctorMapper.OneEntityToDto(doctorEntity)
            };
        }

        public async Task<ResponseDto<List<DoctorResponseDto>>> GetByHealthCenterIdAsync(string healthCenterId)
        {
            var doctorEntities = await _context.Doctors
                .Where(d => d.HealthCenterId == healthCenterId)
                .OrderBy(d => d.FirstName)
                .ThenBy(d => d.LastName)
                .ToListAsync();

            var doctorsDto = DoctorMapper.ListEntityToListDto(doctorEntities);

            return new ResponseDto<List<DoctorResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = doctorsDto
            };
        }
    }
}