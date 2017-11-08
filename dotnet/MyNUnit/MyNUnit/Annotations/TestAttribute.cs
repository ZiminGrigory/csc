using System;

namespace MyNUnit.Annotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Test : Attribute
    {
        public Type Expected { get; set; }
        public bool Ignore { get; set; }
    }
}