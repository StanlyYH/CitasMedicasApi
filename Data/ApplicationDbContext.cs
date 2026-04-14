using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

       
    }
}