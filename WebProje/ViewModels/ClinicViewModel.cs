using System.ComponentModel.DataAnnotations;

namespace WebProje.ViewModels;

public class ClinicViewModel
{
    [Required]
    public string ClinicName { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<int> hospitals { get; set; } = new List<int>();
    
    [Required]
    public int speciality { get; set; }
    
}