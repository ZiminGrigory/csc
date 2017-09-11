using System;
using MyNUnit.Annotations;

namespace MyNUnit.ExcInAfterClass
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

        [AfterClass]
        public void TestAfterClass()
        {
            Console.WriteLine("Class1.TestAfterClass");
            throw new NullReferenceException();
        }
    }
}
