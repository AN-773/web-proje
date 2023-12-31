using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using WebProje.ViewModels;

namespace WebProje.Controllers;

[Authorize(Roles = "Doctor")]
public class DoctorsController : Controller
{
    public ApplicationContext _context;

    public DoctorsController(ApplicationContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Doctor doctor = (from d in _context.Doctors
            where d.UserId == userId
            select d).First();
        return View(doctor);
    }

    public IActionResult CreateWorkingTime()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateWorkingTime(WorkingTimesViewModel workingTimes)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (ModelState.IsValid)
        {
            List<WorkingTimes> workingTimesList = (from wt in _context.WorkingTimes
                where wt.UserId == userId
                select wt).ToList();
            foreach (var day in workingTimes.DaysOfWeek)
            {
                foreach (var wt in workingTimesList)
                {
                    if (wt.DaysOfWeek.Contains(day))
                    {
                        if (wt.StartTime <= workingTimes.StartTime && workingTimes.StartTime <= wt.EndTime)
                        {
                            TempData["error"] = true;
                            return RedirectToAction("CreateWorkingTime");
                        }
                        else if (wt.StartTime <= workingTimes.EndTime && workingTimes.EndTime <= wt.EndTime)
                        {
                            TempData["error"] = true;
                            return RedirectToAction("CreateWorkingTime");
                        }
                        else if (workingTimes.StartTime <= wt.StartTime && wt.StartTime <= workingTimes.EndTime)
                        {
                            TempData["error"] = true;
                            return RedirectToAction("CreateWorkingTime");
                        }
                        else if (workingTimes.StartTime <= wt.EndTime && wt.EndTime <= workingTimes.EndTime)
                        {
                            TempData["error"] = true;
                            return RedirectToAction("CreateWorkingTime");
                        }
                    }
                }
            }

            _context.WorkingTimes.Add(new WorkingTimes
            {
                DaysOfWeek = string.Join(",", workingTimes.DaysOfWeek),
                StartTime = workingTimes.StartTime,
                EndTime = workingTimes.EndTime,
                UserId = userId
            });
            _context.SaveChanges();
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            TempData["error"] = true;
        }

        return RedirectToAction("CreateWorkingTime");
    }

    public IActionResult Appointments()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var appointments = _context.Appointments
            .Include(a => a.HospitalClinic)
            .Include(a => a.HospitalClinic.Hospital)
            .Include(a => a.HospitalClinic.Clinic)
            .Where(a => a.DoctorUserId == userId).ToList();
        return View(appointments);
    }

    public IActionResult ListWorkingTimes()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        List<WorkingTimes> workingTimes = (
            from wt in _context.WorkingTimes
                .Include(h => h.User)
            where wt.UserId == userId
            select wt).ToList();
        return View(workingTimes);
    }

    public IActionResult UpdateSessionTime(int sessionTime)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Doctor doctor = (from d in _context.Doctors
            where d.UserId == userId
            select d).First();
        doctor.SessionTime = sessionTime;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}