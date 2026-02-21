using CareWell.HealthcareSystem.Models;

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

    public Task UpdateAsync(Patient patient)
    {
        return Task.CompletedTask;
    }
}