using CitasMedicasApi.Constants;
using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.HealthCenters;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class HealthCenterService : IHealthCenterService
    {
        private readonly ApplicationDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public HealthCenterService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PageDto<List<HealthCenterResponseDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<HealthCenterEntity> healthCentersQuery = _context.HealthCenters;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                healthCentersQuery = healthCentersQuery.Where(x =>
                    (x.Name + " " + x.Municipality + " " + x.Department).Contains(searchTerm));
            }

            int totalRows = await healthCentersQuery.CountAsync();

            var healthCenterEntities = await healthCentersQuery
                .OrderBy(x => x.Name)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var healthCentersDto = healthCenterEntities
                .Select(HealthCenterMapper.ToResponseDto)
                .ToList();

            return new ResponseDto<PageDto<List<HealthCenterResponseDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<HealthCenterResponseDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = healthCentersDto,
                    HasNextPage = page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<HealthCenterResponseDto>> GetOneByIdAsync(string id)
        {
            var healthCenterEntity = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenterEntity is null)
            {
                return new ResponseDto<HealthCenterResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false
                };
            }

            return new ResponseDto<HealthCenterResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = HealthCenterMapper.ToResponseDto(healthCenterEntity)
            };
        }

        public async Task<ResponseDto<HealthCenterResponseDto>> CreateAsync(CreateHealthCenterDto dto)
        {
            bool healthCenterExists = await _context.HealthCenters.AnyAsync(h =>
                h.Name == dto.Name && h.Address == dto.Address);

            if (healthCenterExists)
            {
                return new ResponseDto<HealthCenterResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Message = "A health center with the same name and address already exists.",
                    Status = false
                };
            }

            HealthCenterEntity healthCenterEntity = HealthCenterMapper.ToEntity(dto);

            _context.HealthCenters.Add(healthCenterEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<HealthCenterResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = HealthCenterMapper.ToResponseDto(healthCenterEntity)
            };
        }

        public async Task<ResponseDto<HealthCenterResponseDto>> EditAsync(string id, UpdateHealthCenterDto dto)
        {
            var healthCenterEntity = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenterEntity is null)
            {
                return new ResponseDto<HealthCenterResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            HealthCenterMapper.UpdateEntity(healthCenterEntity, dto);

            _context.HealthCenters.Update(healthCenterEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<HealthCenterResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = HealthCenterMapper.ToResponseDto(healthCenterEntity)
            };
        }

        public async Task<ResponseDto<HealthCenterResponseDto>> DeleteAsync(string id)
        {
            var healthCenterEntity = await _context.HealthCenters.FirstOrDefaultAsync(h => h.Id == id);

            if (healthCenterEntity is null)
            {
                return new ResponseDto<HealthCenterResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            _context.HealthCenters.Remove(healthCenterEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<HealthCenterResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = HealthCenterMapper.ToResponseDto(healthCenterEntity)
            };
        }
    }
}