namespace Corporation.Infrastructure.Utilities.Exceptions;

public class IdenticalSalaryException : Exception
{
    public IdenticalSalaryException(string message) : base(message) { }
}

