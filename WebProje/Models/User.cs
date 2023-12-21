using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebProje.Models;

public class User: IdentityUser
{
    [Required] public string Name { get; set; } = null!;
    
    public string? ImageName { get; set; } = null;
    
    public ICollection<WorkingTimes> WorkingTimes { set; get; } = null!;


    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}