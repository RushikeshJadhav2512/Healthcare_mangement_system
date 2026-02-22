using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Services;

public interface IPatientService
{
    Task<Patient> RegisterPatientAsync(string firstName, string lastName,
        DateTime dob, string email, string phone);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(Guid id);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);
}