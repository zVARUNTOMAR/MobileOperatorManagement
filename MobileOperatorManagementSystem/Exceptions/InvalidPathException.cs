using System;

namespace CustomExceptions
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException(string message) : base(message) { 
            
        }
    }
}
