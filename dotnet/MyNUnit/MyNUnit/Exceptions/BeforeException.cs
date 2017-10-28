using System;

namespace MyNUnit.Exceptions
{
    [Serializable]
    public class BeforeException : Exception
    {
        public BeforeException() { }
        public BeforeException(string message) : base(message) { }
        public BeforeException(string message, Exception inner) : base(message, inner) { }

        protected BeforeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}