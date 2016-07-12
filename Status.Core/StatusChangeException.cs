using System;

namespace Status.Core
{
    public class StatusChangeException : Exception
    {
        public StatusChangeException(string message) 
            : base(message)
        {
        }
    }
}