using System;

namespace MyNUnit.Annotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeClass : Attribute
    {
    }
}