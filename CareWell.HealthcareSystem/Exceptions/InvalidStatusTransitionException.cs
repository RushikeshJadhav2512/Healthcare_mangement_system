namespace CareWell.HealthcareSystem.Exceptions
{
    public class InvalidStatusTransitionException : Exception
    {
        public InvalidStatusTransitionException(string message) : base(message)
        {
        }
    }
}