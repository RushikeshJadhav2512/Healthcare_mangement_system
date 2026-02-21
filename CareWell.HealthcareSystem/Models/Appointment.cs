namespace CareWell.HealthcareSystem.Models;

public enum AppointmentStatus
{
    Scheduled,
    CheckedIn,
    Completed,
    Cancelled,
    NoShow
}

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public DateTime AppointmentDate { get; private set; }
    public string DoctorName { get; private set; }
    public AppointmentStatus Status { get; private set; }

    public Appointment(Guid patientId, DateTime date, string doctor)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        AppointmentDate = date;
        DoctorName = doctor;
        Status = AppointmentStatus.Scheduled;
    }

    public void UpdateStatus(AppointmentStatus newStatus)
    {
        if (Status == AppointmentStatus.Completed || Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot modify finalized appointment.");

        if (Status == AppointmentStatus.Scheduled && newStatus == AppointmentStatus.Completed)
            throw new InvalidOperationException("Must check-in before completing.");

        Status = newStatus;
    }
}