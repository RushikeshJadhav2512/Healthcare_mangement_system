using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private static readonly List<Appointment> _appointments = new();

    public Task AddAsync(Appointment appointment)
    {
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    public Task<Appointment?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));
    }

    public Task<bool> IsSlotAvailable(DateTime date, string doctor)
    {
        var exists = _appointments.Any(a =>
            a.DoctorName == doctor &&
            a.AppointmentDate == date &&
            a.Status == AppointmentStatus.Scheduled);

        return Task.FromResult(!exists);
    }
    public async Task UpdateAsync(Appointment appointment)
{
    // If using in-memory list, nothing needed
    await Task.CompletedTask;
}
}