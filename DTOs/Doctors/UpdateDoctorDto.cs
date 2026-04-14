namespace CitasMedicasApi.DTOs.Doctors
{
    public class UpdateDoctorDto
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
