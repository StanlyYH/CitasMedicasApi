using CitasMedicasApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

       public DbSet<PatientEntity> Patients { get; set; }
    }
}