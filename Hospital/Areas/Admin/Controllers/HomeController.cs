using System.Numerics;
using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hospital.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewDoctor()
        {
            var doctors = _context.Doctors;
            ViewBag.doctors = doctors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult NewDoctor(Doctor doctor,IFormFile? Img)
        {
            if (Img is not null && Img.Length>0)
            {
                var ImgName=Guid.NewGuid().ToString() +Path.GetExtension(Img.FileName);
                var path=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images",ImgName);
                using(var stream = System.IO.File.Create(path))
                {
                    Img.CopyTo(stream);
                }
                doctor.Img = ImgName;
            }
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index", "Doctors");
        }
        public IActionResult Edit(int id)
        {
            var findDoctor = _context.Doctors.Find(id);
            var doctors = _context.Doctors;
            ViewBag.doctors = doctors.ToList();
            return View(findDoctor);
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor, IFormFile? Img)
        {
            var DoctorInDb = _context.Doctors.AsNoTracking().FirstOrDefault(e => e.Id == doctor.Id);

            if (DoctorInDb is not null)
            {

                if (Img is not null && Img.Length > 0)
                {
                    var ImgName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", ImgName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        Img.CopyTo(stream);
                    }

                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", DoctorInDb.Img);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    doctor.Img = ImgName;
                }
            }
            else
            {
                doctor.Img = DoctorInDb.Img;
            }
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index", "Doctors");
        }
        public IActionResult Delete(int id)
        {
            var findDoctor = _context.Doctors.Find(id);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", findDoctor.Img);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            _context.Doctors.Remove(findDoctor);
            _context.SaveChanges();
            return RedirectToAction("Index", "Doctors");
        }

    }
}
