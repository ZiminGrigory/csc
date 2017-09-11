using System;
using MyNUnit.Annotations;

namespace MyNUnit.ExcInTestOnly
{
    public class Class1
    {
        public Class1()
        {
            Console.WriteLine("Class1");
        }

        [Before]
        public void BeforeTest()
        {
            Console.WriteLine("Class1.BeforeTest");
        }

        [After]
        public void AfterTest()
        {
            Console.WriteLine("Class1.AfterTest");
        }

        [Test]
        public void TestOne()
        {
            System.Threading.Thread.Sleep(255);
            Console.WriteLine("Class1.TestOne");
        }

        [Test(Ignore = true)]
        public void TestIgnored()
        {
        }

        [Test(Expected = typeof(NullReferenceException))]
        public void TestExpectedNullReferenceException()
        {
            Console.WriteLine("Class1.TestExpectedNullReferenceException");
            throw new NullReferenceException("PANIC");
        }

        [Test]
        public void TestNullReferenceException()
        {
            Console.WriteLine("Class1.TestNullReferenceException");
            throw new NullReferenceException("PANIC");
        }
    }
}
