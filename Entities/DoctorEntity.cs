namespace CitasMedicasApi.Entities
{
    public class DoctorEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Specialty { get; set; }
        public string HealthCenterId { get; set; }
        public string Status { get; set; }
    }
}