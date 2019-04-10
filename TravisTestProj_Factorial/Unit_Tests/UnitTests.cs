using NUnit.Framework;
using System;
using TravisTestProj_Factorial;

namespace Unit_Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void IsOkeyTest()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void FactorialOfZero()
        {
            int result = Mathematics.Factorial(0);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void FactorialOfFive()
        {
            int result = Mathematics.Factorial(5);

            Assert.AreEqual(120, result);
        }

        [Test]
        public void IncorrectArgument()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => Mathematics.Factorial(-1)
                );

            Assert.That(
                ex.Message,
                Is.EqualTo("Factorial of negative number is not defined")
                );
        }
    }
}
