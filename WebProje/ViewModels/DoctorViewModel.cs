using System.ComponentModel.DataAnnotations;

namespace WebProje.ViewModels;

public class DoctorViewModel
{
    [Required] public string userId { get; set; }
    public string image { get; set; } = "";

    [Required] public int specialityId { get; set; }

    [Required] public int clinicId { get; set; }

    [Required] public int hospitalId { get; set; }
}