using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MyNUnit.Annotations;
using MyNUnit.Exceptions;
using static System.String;

namespace MyNUnit
{
    public static class MyNUnitRunner
    {
        public static void RunTestsInAssembly(string assemblyPath, TextWriter output, bool isTimeNeeded)
        {
            output.WriteLine($"STARTED: running tests in assembly {Path.GetFileName(assemblyPath)}");
            try
            {
                var assembly = Assembly.LoadFrom(assemblyPath);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }

                    RunTestsInType(type, output, isTimeNeeded);
                }
            }
            catch (Exception e)
            {
                output.WriteLine(e);
            }
            finally
            {
                output.WriteLine($"FINISHED: running tests in assembly {Path.GetFileName(assemblyPath)}");
                output.WriteLine();
                output.Close();
            }
        }

        // suppose, that there are no methods except those which marked TEST which throw exceptions
        private static void RunTestsInType(Type type, TextWriter output, bool isTimeNeeded)
        {
            MethodInfo[] allMethods = type.GetMethods();
            List<MethodInfo> beforeClassMethods =
                allMethods.Where(method => method.GetCustomAttributes<BeforeClass>(false).Any()).ToList();
            List<MethodInfo> afterClassMethods = allMethods.Where(method =>
                method.GetCustomAttributes<AfterClass>(false).Any()).ToList();
            List<MethodInfo> beforeMethods = allMethods.Where(method =>
                method.GetCustomAttributes<Before>(false).Any()).ToList();
            List<MethodInfo> afterMethods = allMethods.Where(method =>
                method.GetCustomAttributes<After>(false).Any()).ToList();
            List<MethodInfo> testMethods = allMethods.Where(method =>
                method.GetCustomAttributes<Test>(false).Any()).ToList();

            beforeClassMethods.Sort((x, y) => Compare(x.Name, y.Name, StringComparison.Ordinal));
            afterClassMethods.Sort((x, y) => Compare(x.Name, y.Name, StringComparison.Ordinal));
            beforeMethods.Sort((x, y) => Compare(x.Name, y.Name, StringComparison.Ordinal));
            afterMethods.Sort((x, y) => Compare(x.Name, y.Name, StringComparison.Ordinal));
            testMethods.Sort((x, y) => Compare(x.Name, y.Name, StringComparison.Ordinal));

            int totalTests = testMethods.Count;
            int succeeded = 0;
            int ignored = 0;
            int failed = 0;
            object testedClass = null;

            if (totalTests == 0)
            {
                return;
            }

            if (!ConstructTestedClass(type, ref testedClass, output))
            {
                return;
            }

            try
            {
                RunMethods(beforeClassMethods, typeof(BeforeClassException), testedClass, output);
                foreach (var testMethod in testMethods)
                {
                    RunMethods(beforeMethods, typeof(BeforeException), testedClass, output);
                    RunTestMethod(
                        testMethod
                        , testedClass
                        , ref succeeded
                        , ref ignored
                        , ref failed
                        , output
                        , isTimeNeeded
                        );
                    RunMethods(afterMethods, typeof(AfterException), testedClass, output);
                }

                RunMethods(afterClassMethods, typeof(AfterClassException), testedClass, output);
            }
            catch (BeforeClassException)
            {
                failed += totalTests;
                output.WriteLine("PROBLEM: there is exception in method which annotated with BeforeClass");
            }
            catch (AfterClassException)
            {
                output.WriteLine("PROBLEM: there is exception in method which annotated with AfterClass");
            }
            catch (BeforeException)
            {
                failed += totalTests;
                output.WriteLine("PROBLEM: there is exception in method which annotated with Before");
            }
            catch (AfterException)
            {
                output.WriteLine("PROBLEM: there is exception in method which annotated with After");
            }
            finally
            {
                output.WriteLine($"Total number of tests: {totalTests}");
                output.WriteLine($"Succeeded:             {succeeded}");
                output.WriteLine($"Ignored:               {ignored}");
                output.WriteLine($"Failed:                {failed}");
            }

        }

        private static void RunMethods(
            IEnumerable<MethodInfo> methods
            , Type exceptionType
            , object testedClass
            , TextWriter output
            )
        {
            MethodInfo currentMethod = null;
            try
            {
                foreach (var method in methods)
                {
                    currentMethod = method;
                    method.Invoke(method.IsStatic ? null : testedClass, null);
                }
            }
            catch (TargetInvocationException exception)
            {
                WriteAboutException(currentMethod, exception, output);
                ConstructorInfo ctor = exceptionType
                    .GetConstructors()
                    .FirstOrDefault(constructor => constructor.GetParameters().Length == 2);

                if (ctor != null)
                {
                    throw (Exception) ctor.Invoke(new[] {(object) null, exception});
                }

                throw;
            }
        }

        private static void RunTestMethod(
            MethodInfo testMethod
            , object testedClass
            , ref int succeeded
            , ref int ignored
            , ref int failed
            , TextWriter output
            , bool isTimeNeeded
            )
        {
            var success = false;
            var testAttribute = testMethod.GetCustomAttribute<Test>();
            if (testAttribute.Ignore)
            {
                output.WriteLine($"TEST {testMethod.Name} did not started (IGNORED)");
                ++ignored;
                return;
            }

            Type expectedExceptionType = testAttribute.Expected;
            var mTimer = Stopwatch.StartNew();
            try
            {
                testMethod.Invoke(testMethod.IsStatic ? null : testedClass, null);
                success = true;
            }
            catch (TargetInvocationException exception)
            {
                if (expectedExceptionType != null && expectedExceptionType == exception.InnerException?.GetType())
                {
                    success = true;
                }
                else
                {
                    ++failed;
                    output.WriteLine($"TEST {testMethod.Name} FAILED, because:");
                    WriteAboutException(testMethod, exception, output);
                }
            }
            finally
            {
                if (success)
                {
                    ++succeeded;
                    if (isTimeNeeded)
                    {
                        output.WriteLine(
                            $"TEST {testMethod.Name} SUCCESSFULLY FINISED "
                            + $"in {mTimer.ElapsedMilliseconds} ms"
                        );
                    }
                    else
                    {
                        output.WriteLine($"TEST {testMethod.Name} SUCCESSFULLY FINISED");
                    }
                }
            }
        }

        private static bool ConstructTestedClass(Type type, ref object testedClass, TextWriter output)
        {
            ConstructorInfo ctor = type.GetConstructors()
                .FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (ctor == null)
            {
                output.WriteLine($"There is no default ctor of type {type.FullName}");
                return false;
            }

            testedClass = ctor.Invoke(new object[0]);
            return true;
        }

        private static void WriteAboutException(
            MethodInfo method
            , TargetInvocationException exception
            , TextWriter output
            )
        {
            output.WriteLine($"UNHANDLED EXCEPTION: {exception.Message}");
            if (exception.InnerException != null)
            {
                output.WriteLine($"WITH EXCEPTION: {exception.InnerException.GetType().FullName}");
                output.WriteLine($"WITH EXCEPTION MSG: {exception.InnerException.Message}");
            }

            output.WriteLine($"GET FROM {method.Name}");
        }
    }
}
