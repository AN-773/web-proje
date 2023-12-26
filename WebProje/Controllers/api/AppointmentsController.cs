using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;
using WebProje.utils;

namespace WebProje.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentsController : Controller
{
    private ApplicationContext _context;

    public AppointmentsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("AvailableAppointments")]
    public IEnumerable<DateTime> getAvailableAppointments(int doctorId, int month, int year, int day)
    {
        var d = _context.Doctors.First(d => d.Id == doctorId);
        var appointments = _context.Appointments.Where(a => a.DoctorUserId == d.UserId).ToList();
        var workingTimes = _context.WorkingTimes.Where(w => w.UserId == d.UserId).ToList();
        return new Scheduler().GenerateSchedule(workingTimes, appointments, d.SessionTime, month, year, day);
    }
    
    [HttpPost("MakeAppointment")]
    public IActionResult MakeAppointment([FromForm(Name = "doctorId")]int doctorId, [FromForm(Name = "date")] string date)
    {
        var dateAndTime = DateTime.Parse(date);
        Console.WriteLine(doctorId);
        Console.WriteLine(dateAndTime);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var d = _context.Doctors.First(d => d.Id == doctorId);
        var hospitalClinic = _context.HospitalClinics.First(hc => hc.ClinicId == d.ClinicId && hc.HospitalId == d.HospitalId);
        var appointments = _context.Appointments.Where(a => a.DoctorUserId == d.UserId).ToList();
        var workingTimes = _context.WorkingTimes.Where(w => w.UserId == d.UserId).ToList();
        var schedule = new Scheduler().GenerateSchedule(workingTimes, appointments, d.SessionTime, dateAndTime.Month, dateAndTime.Year, dateAndTime.Day);
        if (schedule.Contains(dateAndTime))
        {
            var appointment = new Appointment()
            {
                DateAndTime = dateAndTime,
                DoctorUserId = d.UserId,
                UserId = userId,
                HospitalClinicId = hospitalClinic.Id
            };
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return Ok();
        }
        return BadRequest();
    }
   
}