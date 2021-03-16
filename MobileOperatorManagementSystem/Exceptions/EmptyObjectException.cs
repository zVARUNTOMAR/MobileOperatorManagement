using System;

namespace CustomExceptions
{
    public class EmptyObjectException : Exception
    {
        public EmptyObjectException(string message) : base(message) { 
            
        }
    }
}
