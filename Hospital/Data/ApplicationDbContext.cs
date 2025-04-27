using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SN34IHH;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
