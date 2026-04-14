using CitasMedicasApi.Constants;
using CitasMedicasApi.Data;
using CitasMedicasApi.Dtos.Patients;
using CitasMedicasApi.DTOs.Common;
using CitasMedicasApi.DTOs.Patients;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Mappers;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public PatientService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

         public async Task<ResponseDto<PageDto<List<PatientDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<PatientEntity> patientsQuery = _context.Patients;  //*Agregar Include(p => p.citas);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                patientsQuery = patientsQuery.Where( x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(searchTerm));
            }

            int totalRows = await patientsQuery.CountAsync();

            var patientEntity = await patientsQuery.OrderBy(x => x.FirstName).Skip(startIndex).Take(pageSize).ToListAsync();

            var personsDto = PatientMapper.ListEntityToListDto(patientEntity);

            return new ResponseDto<PageDto<List<PatientDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<PatientDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = personsDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                        page < (int)Math.Ceiling((double)totalRows / pageSize), 
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<PatientDto>> GetOneByIdAsync(string id)
        {
            var pacienteEntity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            //*ToDo Agregar el include de p.Citas al hacer el merge en Testing
            // var pacienteEntity = await _context.Pacientes.Include(p =>p.Citas)
            //     .FirstOrDefaultAsync(p => p.Id == id);

            if(pacienteEntity is null)
            {
                return new ResponseDto<PatientDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,                    
                };
            }

            return new ResponseDto<PatientDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new PatientDto
                {
                    Id = pacienteEntity.Id,
                    DNI = pacienteEntity.DNI,
                    FirstName = pacienteEntity.FirstName,
                    LastName = pacienteEntity.LastName,
                    BirthDate = pacienteEntity.BirthDate,
                    Gender = pacienteEntity.Gender,
                    Phone = pacienteEntity.Phone,
                    Address = pacienteEntity.Address
                }
            };
        }


        public async Task<ResponseDto<PatientResponseDto>> CreateAsync(CreatePatientDto dto)
        {
            //*Valida si el paciente ya existe
            bool pacienteExiste = await _context.Patients.AnyAsync(p=> p.DNI == dto.DNI);

            if (pacienteExiste)
            {
                return new ResponseDto<PatientResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Message = "Ya existe un paciente registrado con ese DNI",
                    Status = false
                };
            }

            //*Si el paciente no existe procede a crear 
            PatientEntity patientEntity = PatientMapper.CreateDtoToEntity(dto);

            _context.Patients.Add(patientEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<PatientResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new PatientResponseDto
                {
                    Id = patientEntity.Id
                }
            };
        }

         public async Task<ResponseDto<PatientResponseDto>> EditAsync(string id, UpdatePatientDto dto)
        {
            var patientEntity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if(patientEntity is null)
            {
                return new ResponseDto<PatientResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,

                };
            }

            var patientEntityUpdated = PatientMapper.EditDtoToEntity(patientEntity, dto);

            _context.Patients.Update(patientEntityUpdated);
            await _context.SaveChangesAsync();

            return new ResponseDto<PatientResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new PatientResponseDto
                {
                    Id = id
                }

            };
        }


        public async Task<ResponseDto<PatientResponseDto>> DeleteAsync(string id)
        {
            var patientEntity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if(patientEntity is null)
            {
                return new ResponseDto<PatientResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,

                };
            }

            _context.Patients.Remove(patientEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PatientResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new PatientResponseDto
                {
                    Id = id
                }
            };
           
        }




    }
}