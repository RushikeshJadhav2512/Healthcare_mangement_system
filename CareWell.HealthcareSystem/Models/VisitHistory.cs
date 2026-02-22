namespace CareWell.HealthcareSystem.Models;

public class VisitHistory
{
    public int VisitId { get; set; }

    public Guid AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }

    public string? Diagnosis { get; set; }
    public string? TreatmentNotes { get; set; }

    public DateTime VisitDate { get; set; } = DateTime.UtcNow;
}