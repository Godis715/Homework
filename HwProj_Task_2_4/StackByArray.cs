using System;

namespace StackCalc
{
	public class StackByArray : IStack
	{
		private int[] arr;
		private int currentIndex;
		private bool isEmpty;
		private const int arrSize = 100;

		public bool IsEmpty
		{
			get { return isEmpty; }
		}

		public StackByArray()
		{
			arr = new int[arrSize];
			currentIndex = -1;
			isEmpty = true;
		}

		public void Push(int number)
		{
			currentIndex++;
			if (currentIndex >= arrSize)
			{
				throw new IndexOutOfRangeException("Stack overflow.");
			}
			arr[currentIndex] = number;
			isEmpty = false;
		}
		public int Pop()
		{
			if (isEmpty)
			{
				throw new IndexOutOfRangeException();
			}
			int value = arr[currentIndex];
			currentIndex--;
			if (currentIndex < 0)
			{
				isEmpty = true;
			}
			return value;
		}
		
	}
}
