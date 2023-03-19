namespace Corporation.Infrastructure.Utilities.Exceptions;

public class NullOrEmptyException : Exception
{
    public NullOrEmptyException(string message) : base(message) { }
}

