namespace CareWell.HealthcareSystem.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}