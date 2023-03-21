namespace Corporation.Infrastructure.Utilities.Exceptions;

public class InvalidSalaryException : Exception
{
    public InvalidSalaryException(string message) : base(message) { }
}

