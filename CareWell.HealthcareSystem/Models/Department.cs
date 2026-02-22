namespace CareWell.HealthcareSystem.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Doctor>? Doctors { get; set; }
}