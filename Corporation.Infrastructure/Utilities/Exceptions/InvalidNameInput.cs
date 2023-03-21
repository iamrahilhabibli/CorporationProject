namespace Corporation.Infrastructure.Utilities.Exceptions;

public class InvalidNameInput : Exception
{
    public InvalidNameInput(string message) : base(message) { }
}

