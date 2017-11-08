using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNUnit;

namespace MyNUnitTests
{
    [TestClass]
    public class MyNUnitRunnerTests
    {
        private const string PathToGoldenFiles = @"..\Gold";
        private const string PathToAssemblies = @"..\Tests";
        private const string TempFilePath = PathToGoldenFiles + @"\temp.txt";

        [ClassInitialize]
        public static void TestsInit(TestContext testContext)
        {
            var assemblies = new List<string>();

            if (Directory.Exists(PathToAssemblies))
            {
                foreach (var assembly in Directory.GetFiles(PathToAssemblies))
                {
                    assemblies.Add(Path.GetFullPath(assembly));
                }
            }

            foreach (var assembly in assemblies)
            {
                MyNUnitRunner.RunTestsInAssembly(
                    assembly
                    , new StreamWriter(PathToGoldenFiles + $"\\{Path.GetFileNameWithoutExtension(assembly)}.txt")
                    , false
                );
            }
        }

        [TestCleanup]
        public void AfterTest()
        {
            File.Delete(TempFilePath);
        }

        public static void PrepareTempFile(string assembly)
        {
            Console.WriteLine(assembly);
            MyNUnitRunner.RunTestsInAssembly(assembly, new StreamWriter(TempFilePath), false);
        }

        private static bool IsEqualToGold(string assembly)
        {
            var goldFileName = PathToGoldenFiles + "\\" + Path.GetFileNameWithoutExtension(assembly) + ".txt";
            if (!File.Exists(goldFileName))
            {
                return false;
            }

            var goldStrings = File
                .ReadAllText(goldFileName)
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var testStrings = File
                .ReadAllText(TempFilePath)
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return goldStrings.SequenceEqual(testStrings);
        }

        [TestMethod]
        public void TestExceptionInBeforeClassMethod()
        {
            var assembly = Path.GetFullPath(PathToAssemblies) + @"\MyNUnit.ExcInBeforeClass.dll";
            PrepareTempFile(assembly);
            Assert.IsTrue(IsEqualToGold(assembly));
        }

        [TestMethod]
        public void TestExceptionInBeforeMethod()
        {
            var assembly = Path.GetFullPath(PathToAssemblies) + @"\MyNUnit.ExcInBefore.dll";
            PrepareTempFile(assembly);
            Assert.IsTrue(IsEqualToGold(assembly));
        }

        [TestMethod]
        public void TestExceptionInAfterClassMethod()
        {
            var assembly = Path.GetFullPath(PathToAssemblies) + @"\MyNUnit.ExcInAfterClass.dll";
            PrepareTempFile(assembly);
            Assert.IsTrue(IsEqualToGold(assembly));
        }

        [TestMethod]
        public void TestExceptionInAfterMethod()
        {
            var assembly = Path.GetFullPath(PathToAssemblies) + @"\MyNUnit.ExcInAfter.dll";
            PrepareTempFile(assembly);
            Assert.IsTrue(IsEqualToGold(assembly));
        }

        [TestMethod]
        public void TestExceptionInTestOnly()
        {
            var assembly = Path.GetFullPath(PathToAssemblies) + @"\MyNUnit.ExcInTestOnly.dll";
            PrepareTempFile(assembly);
            Assert.IsTrue(IsEqualToGold(assembly));
        }
    }
}