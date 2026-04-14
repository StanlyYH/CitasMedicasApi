namespace CitasMedicasApi.DTOs.Appointments
{
    public class UpdateAppointmentDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}