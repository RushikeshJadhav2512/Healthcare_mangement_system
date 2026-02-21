namespace CareWell.HealthcareSystem.Models;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsActive { get; set; } = true;

    private readonly List<Appointment> _appointments = new();
    public IReadOnlyCollection<Appointment> Appointments => _appointments;

    public Patient(string firstName, string lastName, DateTime dob, string email, string phone)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Email = email;
        PhoneNumber = phone;
    }

    public void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }
}