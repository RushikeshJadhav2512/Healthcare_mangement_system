using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareWell.HealthcareSystem.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllAsync();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null) return NotFound();
        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Doctor doctor)
    {
        var created = await _doctorService.CreateAsync(doctor);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Doctor doctor)
    {
        if (id != doctor.DoctorId) return BadRequest();
        await _doctorService.UpdateAsync(doctor);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _doctorService.DeleteAsync(id);
        return Ok();
    }
}
