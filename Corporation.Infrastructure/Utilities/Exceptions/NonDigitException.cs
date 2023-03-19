namespace Corporation.Infrastructure.Utilities.Exceptions;

public class NonDigitException : Exception
{
    public NonDigitException(string message) : base(message) { }
}

