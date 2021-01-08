using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.lib.Exceptions
{
    public class CustomMessageException : Exception
    {
        public int Code { get; private set; }
        public CustomMessageException(string message, int code = 500) : base(message)
        {
            Code = code;
        }
    }
}
