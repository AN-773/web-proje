using System.ComponentModel.DataAnnotations;

namespace WebProje.ViewModels;

public class SpecialityViewModel
{
    [Required]
    public string name { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<int> hospitals { get; set; } = new List<int>();
    
}