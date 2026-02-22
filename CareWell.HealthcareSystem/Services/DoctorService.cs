using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Repositories;

namespace CareWell.HealthcareSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public Task<Doctor> CreateAsync(Doctor doctor)
    {
        _doctorRepository.AddAsync(doctor);
        return Task.FromResult(doctor);
    }

    public Task DeleteAsync(int id)
    {
        return _doctorRepository.DeleteAsync(id);
    }

    public Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return _doctorRepository.GetAllAsync();
    }

    public Task<Doctor?> GetByIdAsync(int id)
    {
        return _doctorRepository.GetByIdAsync(id);
    }

    public Task UpdateAsync(Doctor doctor)
    {
        return _doctorRepository.UpdateAsync(doctor);
    }
}
