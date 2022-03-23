using System;

namespace PickPoint.Lib.Domain.Exceptions
{
    public class PickPointAccessDeniedException : Exception
    {
        public PickPointAccessDeniedException()
        {
        }

        public PickPointAccessDeniedException(string message) : base(message)
        {
        }

        public PickPointAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
