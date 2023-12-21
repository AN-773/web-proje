using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje.Models;

public class Doctor
{
    public int Id { set; get; }
    
    public int SessionTime { set; get; }

    public string UserId { set; get; } = null!;
    
    public int SpecialityId { get; set; }
    
    public int ClinicId { get; set; }
    
    public int HospitalId { get; set; }

    public User UserInfo { set; get; } = null!;

    public Clinic Clinic { get; set; } = null!;

    public Speciality Speciality { get; set; } = null!;
    
    public Hospital Hospital { get; set; } = null!;
    
    public ICollection<Appointment> Appointments { set; get; } = null!;

}