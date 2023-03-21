namespace Corporation.Infrastructure.Utilities.Exceptions;

public class NegativeLimitException : Exception
{
    public NegativeLimitException(string message) : base(message) { }
}

