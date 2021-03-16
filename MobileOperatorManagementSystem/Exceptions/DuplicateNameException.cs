using System;

namespace CustomExceptions
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException(string message) : base(message)
        {
        }
    }
}