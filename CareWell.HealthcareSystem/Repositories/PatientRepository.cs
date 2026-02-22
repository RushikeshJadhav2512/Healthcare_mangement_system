using CareWell.HealthcareSystem.Models;
using System.Linq;

namespace CareWell.HealthcareSystem.Repositories;

public class PatientRepository : IPatientRepository
{
    private static readonly List<Patient> _patients = new();

    public Task AddAsync(Patient patient)
    {
        _patients.Add(patient);
        return Task.CompletedTask;
    }

    public Task<Patient?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Patient>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Patient>>(_patients.ToList());
    }

    public Task UpdateAsync(Patient patient)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var existing = _patients.FirstOrDefault(p => p.Id == id);
        if (existing != null) _patients.Remove(existing);
        return Task.CompletedTask;
    }
}