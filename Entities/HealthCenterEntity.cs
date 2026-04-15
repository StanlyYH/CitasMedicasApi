using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    [Table("health_centers")]
    public class HealthCenterEntity : BaseEntity
    {
        [Required]
        [StringLength(150)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        [Column("municipality")]
        public string Municipality { get; set; }

        [Required]
        [StringLength(100)]
        [Column("department")]
        public string Department { get; set; }

        [Required]
        [StringLength(20)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        public ICollection<DoctorEntity> Doctors { get; set; } = new List<DoctorEntity>();
    }
}