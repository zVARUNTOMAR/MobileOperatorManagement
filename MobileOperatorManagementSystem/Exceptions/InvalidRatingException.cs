using System;

namespace CustomExceptions
{
    public class InvalidRatingException : Exception
    {
        public InvalidRatingException(string message) : base(message) { 
            
        }
    }
}
