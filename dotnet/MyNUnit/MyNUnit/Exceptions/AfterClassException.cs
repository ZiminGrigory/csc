using System;

namespace MyNUnit.Exceptions
{
    [Serializable]
    public class AfterClassException : Exception
    {
        public AfterClassException() { }
        public AfterClassException(string message) : base(message) { }
        public AfterClassException(string message, Exception inner) : base(message, inner) { }

        protected AfterClassException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}