namespace Corporation.Infrastructure.Utilities.Exceptions;

public class CapacityLimitException : Exception
{
    public CapacityLimitException(string message) : base(message) { }
}

