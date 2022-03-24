namespace PickPoint.Lib.Domain.Exceptions;

public class PickPointAccessTokenIssuanceException : Exception
{
    public PickPointAccessTokenIssuanceException()
    {
    }

    public PickPointAccessTokenIssuanceException(string message) : base(message)
    {
    }

    public PickPointAccessTokenIssuanceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}