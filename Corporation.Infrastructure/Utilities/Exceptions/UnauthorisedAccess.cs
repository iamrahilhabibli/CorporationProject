namespace Corporation.Infrastructure.Utilities.Exceptions;

public class UnauthorisedAccess : Exception
{
    public UnauthorisedAccess(string message) : base(message) { }
}

