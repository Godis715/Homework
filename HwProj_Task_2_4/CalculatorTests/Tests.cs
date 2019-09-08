using System;
using StackCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void AddingTwoNumbers()
		{
			var stack1 = new StackCalc.StackByArray();

			var calc1 = new StackCalc.Calculator(stack1);

			calc1.Put(3);
			calc1.Put(4);
			calc1.Div();
			int answer = calc1.GetTop();
			Assert.AreEqual(answer, 0);

			calc1.Put(3);
			calc1.Add();
			answer = calc1.GetTop();
			Assert.AreEqual(answer, 3);

			calc1.Put(4);
			calc1.Sub();
			answer = calc1.GetTop();
			Assert.AreEqual(answer, -1);

			calc1.Put(5);
			calc1.Mul();
			answer = calc1.GetTop();
			Assert.AreEqual(answer, -5);
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void ActionOnCalculatorWithOneNumber() {
			var stack = new StackCalc.StackByNodes();
			var calc = new StackCalc.Calculator(stack);

			calc.Put(5);
			calc.Mul();
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void ActionOnCalculatorWithZeroNumbers()
		{
			var stack = new StackCalc.StackByNodes();
			var calc = new StackCalc.Calculator(stack);

			calc.Mul();
		}

		[TestMethod]
		public void TwoInPowerOfTen()
		{
			var stack = new StackCalc.StackByNodes();
			var calc = new StackCalc.Calculator(stack);

			for (int i = 0; i < 10; ++i)
			{
				calc.Put(2);
			}

			for (int i = 0; i < 9; ++i)
			{
				calc.Mul();
			}

			Assert.AreEqual(calc.GetTop(), 1024);
		}
	}
}
