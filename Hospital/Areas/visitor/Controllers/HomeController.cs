using System.Diagnostics;
using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Areas.visitor.Controllers
{
    [Area("visitor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookAnAppointment(string? DoctorNameOrSpecialization)
        {
            IQueryable<Doctor> Doctors = _context.Doctors;
            if (DoctorNameOrSpecialization is not null)
            {
                Doctors = Doctors.Where(e => e.Name == DoctorNameOrSpecialization || e.Specialization == DoctorNameOrSpecialization);
            }
            return View(Doctors.ToList());
        }
        public IActionResult formAppointment(int id)
        {
            var doctor = _context.Doctors.Find(id);

            return View(doctor);

        }
        public IActionResult DataDis(Patient patient)
        {
            _context.patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("BookAnAppointment");
        }
        public IActionResult AllData()
        {
            var patient = _context.patients;
            return View(patient.ToList());
        }
        public IActionResult Edit(int id)
        {
            var patient = _context.patients.Find(id);
            return View(patient);
        }
        public IActionResult UpdateEdit(Patient patient)
        {
            _context.patients.Update(patient);
            _context.SaveChanges();
            return RedirectToAction("AllData");
        }
        public IActionResult Delete(int id)
        {
            var patient = _context.patients.Find(id);
            _context.patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("AllData");

        }
       

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
