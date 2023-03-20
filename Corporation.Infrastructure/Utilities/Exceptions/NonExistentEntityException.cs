namespace Corporation.Infrastructure.Utilities.Exceptions;

public class NonExistentEntityException : Exception
{
    public NonExistentEntityException(string message) : base(message) { }
}

