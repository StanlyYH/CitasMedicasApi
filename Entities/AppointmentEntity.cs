using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    [Table("appointments")]
    public class AppointmentEntity : BaseEntity
    {
        [Required]
        [Column("patient_id")]
        public string PatientId { get; set; }

        [Required]
        [Column("doctor_id")]
        public string DoctorId { get; set; }

        [Required]
        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(10)]
        [Column("appointment_time")]
        public string AppointmentTime { get; set; }

        [Required]
        [StringLength(250)]
        [Column("reason")]
        public string Reason { get; set; }

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; }

        [StringLength(500)]
        [Column("notes")]
        public string Notes { get; set; }

        [ForeignKey(nameof(PatientId))]
        public PatientEntity Patient { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public DoctorEntity Doctor { get; set; }
    }
}