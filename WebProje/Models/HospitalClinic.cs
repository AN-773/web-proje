namespace WebProje.Models;

public class HospitalClinic
{
    public int Id { get; set; }
    
    public int HospitalId { get; set; }
    
    public int ClinicId { get; set; }

    public Hospital Hospital { get; set; } = null!;

    public Clinic Clinic { get; set; } = null!;
}