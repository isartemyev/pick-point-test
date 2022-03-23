using System;

namespace PickPoint.Lib.Domain.Exceptions
{
    public class PickPointUnauthorizedAccessException : Exception
    {
        public PickPointUnauthorizedAccessException()
        {
        }

        public PickPointUnauthorizedAccessException(string message) : base(message)
        {
        }

        public PickPointUnauthorizedAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}