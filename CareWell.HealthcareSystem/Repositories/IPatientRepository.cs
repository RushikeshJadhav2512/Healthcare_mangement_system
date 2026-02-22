using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);
}