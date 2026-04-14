namespace CitasMedicasApi.Entities
{
    public class HealthCenterEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Municipality { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}