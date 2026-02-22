using CareWell.HealthcareSystem.Data;
using CareWell.HealthcareSystem.Repositories;
using CareWell.HealthcareSystem.Services;
using CareWell.HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Repositories & services
builder.Services.AddScoped<IPatientRepository, EfPatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, EfAppointmentRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

var app = builder.Build();
// Ensure database is migrated and seed some initial data for development
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Departments.Any())
    {
        db.Departments.Add(new Department { DepartmentName = "Cardiology" });
        db.Departments.Add(new Department { DepartmentName = "General Medicine" });
        db.SaveChanges();
    }

    if (!db.Patients.Any())
    {
        db.Patients.Add(new Patient("John", "Doe", new DateTime(1980, 1, 1), "john@example.com", "1234567890"));
        db.Patients.Add(new Patient("Jane", "Smith", new DateTime(1990, 5, 5), "jane@example.com", "0987654321"));
        db.SaveChanges();
    }
}

// Serve static files (wwwroot) for frontend SPA
app.UseStaticFiles();

// Simple test endpoint to verify DbContext read access
app.MapGet("/api/test/db", async (ApplicationDbContext db) =>
{
    return Results.Ok(new
    {
        Patients = await db.Patients.CountAsync(),
        Departments = await db.Departments.CountAsync()
    });
});

app.MapControllers();

// Fallback to index.html for SPA routes
app.MapFallbackToFile("index.html");

app.Run();
