using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Services;
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
    public async Task<IActionResult> Schedule(Guid patientId, DateTime date, string doctor)
    {
        var appointment = await _appointmentService
            .ScheduleAppointmentAsync(patientId, date, doctor);

        return Ok(appointment);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, AppointmentStatus status)
    {
        await _appointmentService.UpdateAppointmentStatusAsync(id, status);
        return Ok();
    }
}