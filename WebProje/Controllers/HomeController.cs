using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _context = context;

        _logger = logger;
    }

    // [Authorize(Roles = "Admin,User,Doctor")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult MakeAppointment(int doctorId)
    {
        Doctor? d = _context.Doctors
            .Include(d => d.UserInfo)
            .Include(d => d.Hospital)
            .Include(d => d.Clinic)
            .FirstOrDefault(d => d.Id == doctorId);
        if (d == null)
            return new NotFoundResult();
        Console.WriteLine(d.UserInfo.Name);
        return View(d);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult MyAppointments()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var appointments = _context.Appointments
            .Include(a => a.HospitalClinic)
            .Include(a => a.HospitalClinic.Hospital)
            .Include(a => a.HospitalClinic.Clinic)
            .Where(a => a.UserId == userId).ToList();
        return View(appointments);
    }
    
    public IActionResult ChangeLanguage(string lang)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
            new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)}
        );
        return RedirectToAction("Index");
    }
}