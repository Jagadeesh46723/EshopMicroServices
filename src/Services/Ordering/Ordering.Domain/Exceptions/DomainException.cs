namespace Ordering.Domain.Exceptions
{
    [Serializable]
    internal class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string? message) : base($"Domain Exception: {message}")
        {
        }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}