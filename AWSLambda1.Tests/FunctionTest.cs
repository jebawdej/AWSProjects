using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using AWSLambda1;

namespace AWSLambda1.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            Student student = new Student() { Name = "Wim de Jong", Email = "wim@email.com" };
            var result = function.FunctionHandler(student, context);

            Assert.Equal($"Name={student.Name}, Email={student.Email}", result);
        }
    }
}
