using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.CustomException
{
    public class MessageException : Exception
    {
        public MessageException() :base()
        { }
        public MessageException(string message):base(message)
        { }

        public MessageException(string message, Exception innerException) : base(message, innerException)
        { }
        protected MessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public MessageException(string code, string message) { }
        public MessageException(string code, string message, string detail) { }
        

        public string Code { get; }
        public string Detail { get; }
        public bool Handled { get; set; }
    }
}
