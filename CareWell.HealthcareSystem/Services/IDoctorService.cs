using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor> CreateAsync(Doctor doctor);
    Task UpdateAsync(Doctor doctor);
    Task DeleteAsync(int id);
}
