using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CitasMedicasApi.Entities;

namespace CitasMedicasApi.Entities
{
    public class HealthCenterEntity : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("municipality")]
        public string Municipality { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("department")]
        public string Department { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("status")]
        public string Status { get; set; }
    }
}