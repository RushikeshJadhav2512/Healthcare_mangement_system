using CareWell.HealthcareSystem.Data;
using CareWell.HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CareWell.HealthcareSystem.Repositories;

public class EfPatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _db;

    public EfPatientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Patient patient)
    {
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _db.Patients.FindAsync(id);
        if (existing != null)
        {
            _db.Patients.Remove(existing);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _db.Patients.AsNoTracking().ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _db.Patients.Include(p => p.Appointments).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(Patient patient)
    {
        _db.Patients.Update(patient);
        await _db.SaveChangesAsync();
    }
}
