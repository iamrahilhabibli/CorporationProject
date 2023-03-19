namespace Corporation.Infrastructure.Utilities.Exceptions;

public class IdenticalNameException : Exception
{
    public IdenticalNameException(string message) : base(message) { }
}

