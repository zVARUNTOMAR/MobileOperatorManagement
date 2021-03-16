﻿using System;

namespace Exceptions
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException(string message) : base(message)
        {
        }
    }
}