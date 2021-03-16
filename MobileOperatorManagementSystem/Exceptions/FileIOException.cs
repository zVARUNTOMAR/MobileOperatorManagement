using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class FileIOException : Exception
    {
        public FileIOException(string message) : base(message) { 
        
        }
    }
}
