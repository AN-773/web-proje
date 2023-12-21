namespace WebProje.Models;

public class HospitalSpecialities
{
    public int Id { get; set; }
    
    public int HospitalId { get; set; }
    
    public int SpecialityId { get; set; }

    public Hospital? Hospital { get; set; } = null;

    public Speciality? Speciality { get; set; } = null;
}