using System;

namespace MyNUnit.Exceptions
{
    [Serializable]
    public class BeforeClassException : Exception
    {
        public BeforeClassException() { }
        public BeforeClassException(string message) : base(message) { }
        public BeforeClassException(string message, Exception inner) : base(message, inner) { }

        protected BeforeClassException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}