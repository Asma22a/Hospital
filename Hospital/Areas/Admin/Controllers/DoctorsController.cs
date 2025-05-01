using Hospital.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context = new();

        
        public IActionResult Index()
        {
            var doctors = _context.Doctors;
            return View(doctors.ToList());
        }
      
       
       
    }
}
