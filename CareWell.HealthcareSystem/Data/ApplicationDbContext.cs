using CareWell.HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CareWell.HealthcareSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; } // Exposing Appointments DbSet for frontend access
    }
}