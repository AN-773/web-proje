namespace WebProje.Models;

public class Clinic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int SpecialityId { get; set; }

    public Speciality? Speciality { get; set; } = null;
    
    public List<Hospital>? Hospitals { get; set; } = null;
}