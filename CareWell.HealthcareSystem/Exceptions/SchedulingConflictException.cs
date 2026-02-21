namespace CareWell.HealthcareSystem.Exceptions
{
    public class SchedulingConflictException : Exception
    {
        public SchedulingConflictException(string message) : base(message)
        {
        }
    }
}