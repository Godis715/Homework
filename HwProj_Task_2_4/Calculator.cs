using System;

namespace StackCalc
{
	public class Calculator
	{
		IStack stack;

		public Calculator(IStack s)
		{
			stack = s;
		}

		public void Put(int number)
		{
			stack.Push(number);
		}

		public int GetTop()
		{
			int number = stack.Pop();
			stack.Push(number);
			return number;
		}

		private Tuple<int, int> GetTwoNumbers()
		{
			int leftNumber;
			int rightNumber;

			if (!stack.IsEmpty)
			{
				rightNumber = stack.Pop();
			}
			else
			{
				throw new IndexOutOfRangeException();
			}
			if (!stack.IsEmpty)
			{
				leftNumber = stack.Pop();
			}
			else
			{
				throw new IndexOutOfRangeException();
			}

			Tuple<int, int> pair = new Tuple<int, int>(leftNumber, rightNumber);
			return pair;
		}

		public void Add()
		{
			Tuple<int, int> pair = GetTwoNumbers();
			stack.Push(pair.Item1 + pair.Item2);
		}

		public void Sub()
		{
			Tuple<int, int> pair = GetTwoNumbers();
			stack.Push(pair.Item1 - pair.Item2);
		}

		public void Mul()
		{
			Tuple<int, int> pair = GetTwoNumbers();
			stack.Push(pair.Item1 * pair.Item2);
		}

		public void Div()
		{
			Tuple<int, int> pair = GetTwoNumbers();
			stack.Push(pair.Item1 / pair.Item2);
		}
	}
}
