using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    [Table("doctors")]
    public class DoctorEntity : BaseEntity
    {
        [Required]
        [StringLength(40)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Column("specialty")]
        public string Specialty { get; set; }

        [Required]
        [Column("health_center_id")]
        public string HealthCenterId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [ForeignKey(nameof(HealthCenterId))]
        public HealthCenterEntity HealthCenter { get; set; }

        public ICollection<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
    }
}