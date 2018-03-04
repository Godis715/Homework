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
			var stack2 = new StackCalc.StackByNodes();

			var calc1 = new StackCalc.Calculator(stack1);
			var calc2 = new StackCalc.Calculator(stack2);

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

			calc2.Put(3);
			calc2.Put(4);
			calc2.Div();
			answer = calc2.GetTop();
			Assert.AreEqual(answer, 0);

			calc2.Put(3);
			calc2.Add();
			answer = calc2.GetTop();
			Assert.AreEqual(answer, 3);

			calc2.Put(4);
			calc2.Sub();
			answer = calc2.GetTop();
			Assert.AreEqual(answer, -1);

			calc2.Put(5);
			calc2.Mul();
			answer = calc2.GetTop();
			Assert.AreEqual(answer, -5);

		}
	}
}
