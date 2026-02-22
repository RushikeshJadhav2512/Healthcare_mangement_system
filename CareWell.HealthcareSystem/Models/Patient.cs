namespace CareWell.HealthcareSystem.Models;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    // Navigation property
    public List<Appointment> Appointments { get; private set; } = new();

    public Patient() { }

    public Patient(string firstName, string lastName, DateTime dob, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Email = email;
        PhoneNumber = phone;
    }

    public void UpdateDetails(string firstName, string lastName, DateTime dob, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Email = email;
        PhoneNumber = phone;
    }

    // Optional: add method to add appointment safely
    public void AddAppointment(Appointment appointment)
    {
        Appointments.Add(appointment);
    }
}