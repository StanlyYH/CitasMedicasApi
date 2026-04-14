namespace CitasMedicasApi.DTOs.HealthCenters
{
    public class HealthCenterResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Municipality { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}