namespace Corporation.Infrastructure.Utilities.Exceptions;

public class InvalidLimitException : Exception
{
    public InvalidLimitException(string message) : base(message) { }
}

