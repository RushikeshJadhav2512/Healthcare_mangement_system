using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
}