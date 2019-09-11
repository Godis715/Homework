using System;
using HwProj_Task_2_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace StackTest1
{
	[TestClass]
	public class StackTests
	{
		[TestMethod]
		public void TestPushAndPop()
		{
			var stack = new HwProj_Task_2_1.Stack();
			stack.Push(1);
			var result = stack.Pop();

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void TestTwoPushAndPop()
		{
			var stack = new HwProj_Task_2_1.Stack();
			stack.Push(1);
			stack.Push(2);

			var result = stack.Pop();

			Assert.AreEqual(result, 2);

			result = stack.Pop();

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void PopOnEmptyStack() {

			var stack = new HwProj_Task_2_1.Stack();
			stack.Pop();
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void OnePushAndTwoPop()
		{
			var stack = new HwProj_Task_2_1.Stack();
			stack.Push(1);
			var result = stack.Pop();

			Assert.AreEqual(result, 1);

			stack.Pop();
		}

		[TestMethod]
		public void SizeChecking() {

			var stack = new HwProj_Task_2_1.Stack();
			var size = stack.Size;
			Assert.AreEqual(size, 0);

			stack.Push(10);
			size = stack.Size;
			Assert.AreEqual(size, 1);

			stack.Push(20);
			size = stack.Size;
			Assert.AreEqual(size, 2);

			stack.Pop();
			size = stack.Size;
			Assert.AreEqual(size, 1);

			stack.Pop();
			size = stack.Size;
			Assert.AreEqual(size, 0);
		}
	}
	
}
