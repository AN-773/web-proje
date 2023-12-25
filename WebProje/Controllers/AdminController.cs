using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using WebProje.ViewModels;

namespace WebProje.Controllers;

public class AdminController : Controller
{
    private ApplicationContext _context;
    
    private readonly UserManager<User> _userManager;


    public AdminController(ApplicationContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateHospital()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateHospital([Bind("Name")] Hospital hospital)
    {
        if (ModelState.IsValid)
        {
            TempData["created"] = true;
            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return RedirectToAction("Index");
        }

        return RedirectToAction("CreateHospital");
    }

    public IActionResult ListHospitals()
    {
        ViewBag.Hospitals = _context.Hospitals.ToList();
        return View();
    }

    public IActionResult DeleteHospital(int id)
    {
        var hospital = _context.Hospitals.Find(id);
        if (hospital != null)
        {
            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();
        }

        return RedirectToAction("ListHospitals");
    }

    public IActionResult CreateGroup()
    {
        return View();
    }

    public IActionResult ListGroups()
    {
        return View();
    }

    public IActionResult CreateDoctor()
    {
        ViewData["users"] = _context.Users.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult CreateDoctor(DoctorViewModel viewModel, [FromForm(Name ="image")] IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            TempData["error"] = true;
            return RedirectToAction("CreateDoctor");
        }

        if (file != null && file.Length > 0 && (file.FileName.EndsWith(".png") || file.FileName.EndsWith(".jpg")))
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            
            viewModel.image = uniqueFileName;
        }
        else
        {
            TempData["error"] = true;
            return RedirectToAction("CreateDoctor");
        }
        
        var user = _context.Users.Find(viewModel.userId);
        user.ImageName = viewModel.image;
        _context.Users.Update(user);
        
        Console.WriteLine("h: " + viewModel.hospitalId);
        var doctor = new Doctor
        {
            UserId = viewModel.userId,
            SpecialityId = viewModel.specialityId,
            ClinicId = viewModel.clinicId,
            HospitalId = viewModel.hospitalId
            
        };
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        _userManager.AddToRoleAsync(user, "Doctor").Wait();
        return RedirectToAction("CreateDoctor");
    }

    public IActionResult ListDoctors()
    {
        return View(_context.Doctors
            .Include(d => d.Speciality)
            .Include(d => d.Hospital)
            .Include(d => d.Clinic)
            .Include(d => d.UserInfo)
            .ToList());
    }
    
    public IActionResult DeleteDoctor(int id)
    {
        var doctor = _context.Doctors.Find(id);
        if (doctor == null) return RedirectToAction("ListDoctors");
        _context.Doctors.Remove(doctor);
        _context.SaveChanges();

        return RedirectToAction("ListDoctors");
    }

    public IActionResult CreateClinic()
    {
        ViewBag.Hospitals = _context.Hospitals.ToList();
        ViewBag.Specialities = _context.Specialities.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult CreateClinic(ClinicViewModel clinic)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("asd " + clinic.ClinicName);
            clinic.ClinicName = clinic.ClinicName.Trim();
            var clinicModel = new Clinic
            {
                Name = clinic.ClinicName,
                SpecialityId = clinic.speciality
            };
            Console.WriteLine("dsa " + clinicModel.Name);
            _context.Clinics.Add(clinicModel);
            _context.SaveChanges();
            foreach (var hospitalId in clinic.hospitals)
            {
                var hospital = _context.Hospitals.Find(hospitalId);
                if (hospital != null)
                {
                    hospital.Clinics.Add(clinicModel);
                    _context.SaveChanges();
                }
            }

            TempData["created"] = true;
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            TempData["error"] = true;
            return RedirectToAction("CreateClinic");
        }

        return RedirectToAction("CreateClinic");
    }

    public IActionResult ListClinics()
    {
        ViewBag.clinics = _context.Clinics.Include(c => c.Hospitals).Include(c => c.Speciality).ToList();
        return View();
    }

    public IActionResult DeleteClinic(int id)
    {
        var clinic = _context.Clinics.Find(id);
        if (clinic == null) return RedirectToAction("ListClinics");
        _context.Clinics.Remove(clinic);
        _context.SaveChanges();

        return RedirectToAction("ListClinics");
    }

    public IActionResult CreateSpeciality()
    {
        ViewBag.Hospitals = _context.Hospitals.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult CreateSpeciality(SpecialityViewModel speciality)
    {
        if (ModelState.IsValid)
        {
            speciality.name = speciality.name.Trim();
            var specialityModel = new Speciality
            {
                Name = speciality.name
            };
            _context.Specialities.Add(specialityModel);
            _context.SaveChanges();
            foreach (var hospitalId in speciality.hospitals)
            {
                var hospital = _context.Hospitals.Find(hospitalId);
                if (hospital != null)
                {
                    hospital.Specialities.Add(specialityModel);
                    _context.SaveChanges();
                }
            }

            TempData["created"] = true;
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            TempData["error"] = true;
            return RedirectToAction("CreateSpeciality");
        }

        return RedirectToAction("CreateSpeciality");
    }

    public IActionResult DeleteSpeciality(int id)
    {
        var speciality = _context.Specialities.Find(id);
        if (speciality != null)
        {
            _context.Specialities.Remove(speciality);
            _context.SaveChanges();
        }

        return RedirectToAction("ListSpecialities");
    }

    public IActionResult ListSpecialities()
    {
        ViewBag.Specialities = _context.Specialities.ToList();
        return View();
    }
}