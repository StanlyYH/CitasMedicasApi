using System.ComponentModel.DataAnnotations;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Entities
{
    public class HealthCenterEntity : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Municipality { get; set; }

        [Required]
        [MaxLength(100)]
        public string Department { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
    }
}