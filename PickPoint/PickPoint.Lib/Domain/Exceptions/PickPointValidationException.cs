namespace PickPoint.Lib.Domain.Exceptions;

public class PickPointValidationException : Exception
{
    public PickPointValidationException()
    {
    }

    public PickPointValidationException(string message) : base(message)
    {
    }

    public PickPointValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}