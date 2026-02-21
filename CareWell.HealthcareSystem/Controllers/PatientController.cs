using CareWell.HealthcareSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareWell.HealthcareSystem.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPatient(
        string firstName, string lastName,
        DateTime dob, string email, string phone)
    {
        var patient = await _patientService.RegisterPatientAsync(
            firstName, lastName, dob, email, phone);

        return Ok(patient);
    }
}