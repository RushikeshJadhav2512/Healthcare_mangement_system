using CareWell.HealthcareSystem.Services;
using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var patient = await _patientService.RegisterPatientAsync(
            dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, dto.Phone);

        var result = new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Email = patient.Email,
            Phone = patient.PhoneNumber,
            IsActive = patient.IsActive
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _patientService.GetAllAsync();
        var dtos = list.Select(p => new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            Phone = p.PhoneNumber,
            IsActive = p.IsActive
        });

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Patient updated)
    {
        if (id != updated.Id) return BadRequest();
        await _patientService.UpdateAsync(updated);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);
        return Ok();
    }
}