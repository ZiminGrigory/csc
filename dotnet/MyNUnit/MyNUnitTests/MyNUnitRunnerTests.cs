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
        private readonly List<string> _assemblies = new List<string>();
        private const string PathToGoldenFiles = @"..\Gold";
        private const string PathToAssemblies = @"..\Tests";

        [TestInitialize]
        public void TestInit()
        {
            if (Directory.Exists(PathToAssemblies))
            {
                foreach (var assembly in Directory.GetFiles(PathToAssemblies))
                {
                    _assemblies.Add(Path.GetFullPath(assembly));
                }
            }

            foreach (var assembly in _assemblies)
            {
                MyNUnitRunner.RunTestsInAssembly(
                    assembly
                    , new StreamWriter(PathToGoldenFiles + $"\\{Path.GetFileNameWithoutExtension(assembly)}.txt")
                    , false
                );
            }
        }

        [TestMethod]
        public void TestRunTestsInAssembly()
        {
            foreach (var assembly in _assemblies)
            {
                const string tempFilePath = PathToGoldenFiles + "\\temp.txt";
                MyNUnitRunner.RunTestsInAssembly(assembly, new StreamWriter(tempFilePath), false);
                var goldFileName = PathToGoldenFiles + "\\" + Path.GetFileNameWithoutExtension(assembly) + ".txt";
                if (!File.Exists(goldFileName))
                {
                    Assert.Fail();
                }

                var goldStrings = File
                    .ReadAllText(goldFileName)
                    .Split(new []{Environment.NewLine}, StringSplitOptions.None);

                var testStrings = File
                    .ReadAllText(goldFileName)
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                File.Delete(tempFilePath);
                var result = goldStrings.SequenceEqual(testStrings);

                Assert.IsTrue(result);
            }
        }
    }
}