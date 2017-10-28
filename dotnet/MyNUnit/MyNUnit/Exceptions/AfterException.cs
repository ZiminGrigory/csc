using System;

namespace MyNUnit.Exceptions
{
    [Serializable]
    public class AfterException : Exception
    {
        public AfterException() { }
        public AfterException(string message) : base(message) { }
        public AfterException(string message, Exception inner) : base(message, inner) { }

        protected AfterException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}