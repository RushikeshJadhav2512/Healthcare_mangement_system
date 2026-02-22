using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Services;
using CareWell.HealthcareSystem.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CareWell.HealthcareSystem.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Schedule([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var appointment = await _appointmentService
            .ScheduleAppointmentAsync(dto.PatientId, dto.AppointmentDate, dto.DoctorName);

        var result = new AppointmentDto
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate,
            DoctorName = appointment.DoctorName,
            Status = (int)appointment.Status
        };

        return CreatedAtAction(nameof(GetAll), result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _appointmentService.GetAllAsync();
        return Ok(list);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, AppointmentStatus status)
    {
        await _appointmentService.UpdateAppointmentStatusAsync(id, status);
        return Ok();
    }
}