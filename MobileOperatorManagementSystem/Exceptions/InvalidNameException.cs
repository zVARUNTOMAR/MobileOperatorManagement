using System;

namespace CustomExceptions
{
    public class InvalidNameExceptions : Exception
    {
        public InvalidNameExceptions(string message) : base(message) { 
            
        }
    }
}
