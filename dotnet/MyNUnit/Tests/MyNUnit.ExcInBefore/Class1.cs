using System;
using MyNUnit.Annotations;

namespace MyNUnit.ExcInBefore
{
    public class Class1
    {
        public Class1()
        {
            Console.WriteLine("Class1");
        }

        [Test]
        public void TestOne()
        {
            Console.WriteLine("Class1.TestOne");
        }

        [Before]
        public void TestBefore()
        {
            Console.WriteLine("Class1.TestBefore");
            throw new NullReferenceException();
        }
    }
}
