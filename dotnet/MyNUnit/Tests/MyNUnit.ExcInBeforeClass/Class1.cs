using System;
using MyNUnit.Annotations;

namespace MyNUnit.ExcInBeforeClass
{
    public class Class1
    {
        public Class1()
        {
            Console.WriteLine("Class1");
        }

        [BeforeClass]
        public void BeforeClassTest()
        {
            Console.WriteLine("Class1.BeforeClassTest");
            throw new NullReferenceException();
        }

        [Test]
        public void TestOne()
        {
            Console.WriteLine("Class1.TestOne");
        }
    }
}
