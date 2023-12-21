namespace WebProje.Models;

public class Speciality
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}