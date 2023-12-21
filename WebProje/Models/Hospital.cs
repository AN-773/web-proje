namespace WebProje.Models;

public class Hospital
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = null!;
    
    public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
    
    public ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
    
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}