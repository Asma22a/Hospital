namespace Hospital.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string? PatientName { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public TimeOnly? AppointmentTime { get; set; }
        public ICollection<Doctor> Doctors { get; } = new List<Doctor>();
    }
}
