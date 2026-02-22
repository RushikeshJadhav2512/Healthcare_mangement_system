using CareWell.HealthcareSystem.Models;
using System.Linq;

namespace CareWell.HealthcareSystem.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private static readonly List<Doctor> _doctors = new();

    public Task AddAsync(Doctor doctor)
    {
        _doctors.Add(doctor);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existing = _doctors.FirstOrDefault(d => d.DoctorId == id);
        if (existing != null) _doctors.Remove(existing);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Doctor>>(_doctors.ToList());
    }

    public Task<Doctor?> GetByIdAsync(int id)
    {
        return Task.FromResult(_doctors.FirstOrDefault(d => d.DoctorId == id));
    }

    public Task UpdateAsync(Doctor doctor)
    {
        return Task.CompletedTask;
    }
}
