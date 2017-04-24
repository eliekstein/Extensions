using System;

namespace SweetHelpers.Exceptions
{
    public static class ExceptionExtensions
    {
        public static Exception GetBottomException(this Exception exception)
        {
            Exception ex = exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

    }
}
