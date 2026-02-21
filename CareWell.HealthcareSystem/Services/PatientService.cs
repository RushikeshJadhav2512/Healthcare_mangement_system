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
}