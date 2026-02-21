using CareWell.HealthcareSystem.Models;

namespace CareWell.HealthcareSystem.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid id);
    Task AddAsync(Appointment appointment);
    Task<bool> IsSlotAvailable(DateTime date, string doctor);
    Task UpdateAsync(Appointment appointment);
}