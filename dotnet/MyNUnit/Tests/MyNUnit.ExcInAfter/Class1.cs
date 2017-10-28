using System;
using MyNUnit.Annotations;

namespace MyNUnit.ExcInAfter
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

        [After]
        public void TestAfter()
        {
            Console.WriteLine("Class1.TestAfter");
            throw new NullReferenceException();
        }
    }
}
