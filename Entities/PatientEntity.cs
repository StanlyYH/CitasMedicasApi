using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    [Table("patients")]
    public class PatientEntity : BaseEntity
    {
        [Required]
        [StringLength(13)]
        [Column("dni")]
        public string DNI { get; set; }

        [Required]
        [StringLength(40)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("address")]
        public string Address { get; set; }
    }
}