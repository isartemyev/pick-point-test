namespace PickPoint.Lib.Domain.Exceptions;

public class PickPointEntityNotFoundException : Exception
{
    public PickPointEntityNotFoundException()
    {
    }

    public PickPointEntityNotFoundException(string message) : base(message)
    {
    }

    public PickPointEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}