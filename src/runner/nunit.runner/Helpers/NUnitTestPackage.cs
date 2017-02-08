// ***********************************************************************
// Copyright (c) 2017 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityCompatNUnit.Framework.Api;
using UnityCompatNUnit.Framework.Interfaces;
using UnityCompatNUnit.Framework.Internal;
using UnityCompatNUnit.Runner.Results;
// Due to a collision with UnityTestTools
using TestResult = UnityCompatNUnit.Framework.Interfaces.ITestResult;

namespace UnityCompatNUnit.Runner
{
    public class NUnitTestPackage
    {
        private readonly HashSet<Assembly> m_testAssemblies = new HashSet<Assembly>();
        private NUnitTestRunResult m_results = null;

        public void AddAssembly(Assembly testAssembly)
        {
            m_testAssemblies.Add(testAssembly);
        }

        public void AddAssemblies(Assembly[] testAssemblies)
        {
            m_testAssemblies.UnionWith(testAssemblies);
        }

        /// <summary>
        /// Executes the tests.
        /// </summary>
        /// <param name="testFilter">
        /// The NUnit.Framework test filter to apply.
        /// </param>
        /// <remarks>
        /// This method does not complete the test run. <see cref="NUnitTestPackage.CompleteTestRun"/> 
        /// </remarks>
        public void ExecuteTests(ITestFilter testFilter)
        {
            if (m_results == null)
            {
                m_results = new NUnitTestRunResult();
            }

            foreach (var assembly in m_testAssemblies)
            {
                NUnitTestAssemblyRunner runner = LoadTestAssembly(assembly);
                TestResult result = runner.Run(TestListener.NULL, testFilter);
                m_results.AddResult(result);
            }
        }

        /// <summary>
        /// Completes the test run.
        /// </summary>
        /// <remarks>
        /// Exposed to allow the same TestPackage to be executed in multiple passes.
        /// This allows us to have more control over invoking tests in a consistent order.
        /// This goes against Unit Testing principles, however it allows us to provide
        /// consistent behavior which is desirable.
        /// </remarks>
        public void CompleteTestRun()
        {
            if (m_results == null)
            {
                //ERROR
                return;
            }
            m_results.CompleteTestRun();
        }

        public NUnitResultSummary GetTestXMLSummary()
        {
            NUnitResultSummary summary = new NUnitResultSummary(m_results);
            return summary;
        }

        private NUnitTestAssemblyRunner LoadTestAssembly(Assembly assembly)
        {
            NUnitTestAssemblyRunner runner = new NUnitTestAssemblyRunner(new DefaultTestAssemblyBuilder());
            runner.Load(assembly, new Dictionary<string, object>());
            return runner;
        }
    }
}