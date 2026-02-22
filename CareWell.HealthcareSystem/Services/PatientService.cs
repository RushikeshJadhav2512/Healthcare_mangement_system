using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Repositories;

namespace CareWell.HealthcareSystem.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient> RegisterPatientAsync(string firstName, string lastName,
        DateTime dob, string email, string phone)
    {
        var patient = new Patient(firstName, lastName, dob, email, phone);
        await _patientRepository.AddAsync(patient);
        return patient;
    }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            return _patientRepository.GetAllAsync();
        }

        public Task<Patient?> GetByIdAsync(Guid id)
        {
            return _patientRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Patient patient)
        {
            return _patientRepository.UpdateAsync(patient);
        }

        public Task DeleteAsync(Guid id)
        {
            return _patientRepository.DeleteAsync(id);
        }
}