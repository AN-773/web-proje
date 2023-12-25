using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private ApplicationContext _context;

        public FiltersController(ApplicationContext context)
        {
            _context = context;
        }

        //Hospitals
        // GET: api/Filters/Hospitals
        [HttpGet("Hospitals")]
        public IEnumerable<Hospital> Get()
        {
            return from hs in _context.Hospitals select hs;
        }

        // Get: api/Filters/Specialities?hospitalId=5
        [HttpGet("Specialities")]
        public IEnumerable<Speciality?> GetSpecialities(int hospitalId)
        {
            return from hs in _context.HospitalSpecialities
                join s in _context.Specialities on hs.SpecialityId equals s.Id
                where hs.HospitalId == hospitalId
                select s;
        }

        // Get: api/Filters/Clinics?hospitalId=5&specialityId=1
        [HttpGet("Clinics")]
        public IEnumerable<Clinic> GetClinics(int hospitalId, int specialityId)
        {
            return from hc in _context.HospitalClinics
                join c in _context.Clinics on hc.ClinicId equals c.Id
                where hc.HospitalId == hospitalId && c.SpecialityId == specialityId
                select c;
        }

        // GET api/Filters/Doctors?hospitalId=5&clinicId=1&specialityId=1
        [HttpGet("Doctors")]
        public IEnumerable<dynamic> GetDoctors(int hospitalId, int clinicId, int specialityId)
        {
            return (from d in _context.Doctors
                join sp in _context.Specialities on d.SpecialityId equals sp.Id
                join hs in _context.Hospitals on d.HospitalId equals hs.Id
                join u in _context.Users on d.UserId equals u.Id
                join cl in _context.Clinics on d.ClinicId equals cl.Id
                where d.HospitalId == hospitalId && d.ClinicId == clinicId && d.SpecialityId == specialityId
                select new
                {
                    Doctor = d,
                    Hospital = hs.Name,
                    Speciality = sp.Name,
                    User = u.Name,
                    Image = u.ImageName,
                    Clinic = cl.Name
                }).ToList();
        }
        
        [HttpGet("AllDoctors")]
        public IEnumerable<dynamic> GetAllDoctors()
        {
            return (from d in _context.Doctors
                join sp in _context.Specialities on d.SpecialityId equals sp.Id
                join hs in _context.Hospitals on d.HospitalId equals hs.Id
                join u in _context.Users on d.UserId equals u.Id
                join cl in _context.Clinics on d.ClinicId equals cl.Id
                select new
                {
                    Doctor = d,
                    Hospital = hs.Name,
                    Speciality = sp.Name,
                    User = u.Name,
                    Image = u.ImageName,
                    Clinic = cl.Name
                }).ToList();
        }
    }
}