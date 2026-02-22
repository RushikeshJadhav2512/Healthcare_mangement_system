using CareWell.HealthcareSystem.Data;
using CareWell.HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CareWell.HealthcareSystem.Repositories;

public class EfAppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _db;

    public EfAppointmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Appointment appointment)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _db.Appointments.AsNoTracking().ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _db.Appointments.FindAsync(id);
    }

    public async Task<bool> IsSlotAvailable(DateTime date, string doctor)
    {
        var exists = await _db.Appointments.AnyAsync(a => a.DoctorName == doctor && a.AppointmentDate == date && a.Status == AppointmentStatus.Scheduled);
        return !exists;
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _db.Appointments.Update(appointment);
        await _db.SaveChangesAsync();
    }
}
