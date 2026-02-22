using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Services;

public interface IAppointmentService
{
    Task<Appointment> ScheduleAppointmentAsync(Guid patientId, DateTime date, string doctor);
    Task UpdateAppointmentStatusAsync(Guid appointmentId, AppointmentStatus status);
    Task<IEnumerable<Appointment>> GetAllAsync();
}