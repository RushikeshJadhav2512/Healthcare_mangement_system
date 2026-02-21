using CareWell.HealthcareSystem.Models;
using CareWell.HealthcareSystem.Repositories;
using CareWell.HealthcareSystem.Exceptions;

namespace CareWell.HealthcareSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
    }

    public async Task<Appointment> ScheduleAppointmentAsync(
        Guid patientId,
        DateTime date,
        string doctor)
    {
        // 🔹 Rule 1 — Check patient eligibility
        var patient = await _patientRepository.GetByIdAsync(patientId);

        if (patient == null || !patient.IsActive)
        {
            throw new PatientEligibilityException(
                "Patient is not eligible for booking."
            );
        }

        // 🔹 Rule 2 — Prevent double booking
        var isAvailable = await _appointmentRepository.IsSlotAvailable(date, doctor);

        if (!isAvailable)
        {
            throw new SchedulingConflictException(
                "Doctor is already booked for this time slot."
            );
        }

        var appointment = new Appointment(patientId, date, doctor);

        await _appointmentRepository.AddAsync(appointment);

        patient.AddAppointment(appointment);
        await _patientRepository.UpdateAsync(patient);

        return appointment;
    }

    public async Task UpdateAppointmentStatusAsync(
        Guid appointmentId,
        AppointmentStatus newStatus)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

        if (appointment == null)
        {
            throw new Exception("Appointment not found");
        }

        // 🔹 Rule 3 — Invalid status transition
        if (appointment.Status == AppointmentStatus.Completed &&
            newStatus == AppointmentStatus.Scheduled)
        {
            throw new InvalidStatusTransitionException(
                "Cannot move from Completed back to Scheduled."
            );
        }

        appointment.UpdateStatus(newStatus);
        await _appointmentRepository.UpdateAsync(appointment);
    }
}