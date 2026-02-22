using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Repositories;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task AddAsync(Doctor doctor);
    Task UpdateAsync(Doctor doctor);
    Task DeleteAsync(int id);
}
