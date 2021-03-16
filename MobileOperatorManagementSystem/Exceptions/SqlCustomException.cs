using System;

namespace CustomExceptions
{
    public class SqlCustomException : Exception
    {
        public SqlCustomException(string message) : base(message) { 
            
        }
    }
}
