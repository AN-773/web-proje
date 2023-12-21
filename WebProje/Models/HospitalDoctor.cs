namespace WebProje.Models;

public class HospitalDoctor
{
    public int Id { get; set; }
    
    public int DoctorId { get; set; }
    
    public int HospitalId { get; set; }

    public Doctor Doctor { get; set; } = null!;
    
    public Hospital Hospital { get; set; } = null!;

}