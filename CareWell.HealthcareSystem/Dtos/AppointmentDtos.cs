using System;

namespace CareWell.HealthcareSystem.Dtos;

public class CreateAppointmentDto
{
    public Guid PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string DoctorName { get; set; } = string.Empty;
}

public class AppointmentDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int Status { get; set; }
}
