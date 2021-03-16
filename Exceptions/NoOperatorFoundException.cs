using System;

namespace Exceptions
{
    public class NoOperatorFoundException : Exception
    {
        public NoOperatorFoundException(string message) : base(message)
        {

        }
    }
}
