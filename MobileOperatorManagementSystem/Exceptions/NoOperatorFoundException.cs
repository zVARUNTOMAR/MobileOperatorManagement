using System;

namespace CustomExceptions
{
    public class NoOperatorFoundException : Exception
    {
        public NoOperatorFoundException(string message) : base(message)
        {

        }
    }
}