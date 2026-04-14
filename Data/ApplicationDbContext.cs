using Microsoft.EntityFrameworkCore;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<HealthCenterEntity> HealthCenters { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }
    }
}