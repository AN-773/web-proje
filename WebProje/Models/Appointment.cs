namespace WebProje.Models;

public class Appointment
{
    public int Id { get; set; }
    
    public int HospitalClinicId { get; set; }
    
    public string UserId { get; set; } = null!;
    
    public string DoctorUserId { get; set; } = null!;
    
    public User User { get; set; } = null!;
    
    public User DoctorUser { get; set; } = null!;
    
    public DateTime DateAndTime { get; set; }

    public HospitalClinic HospitalClinic { get; set; } = null!;
}