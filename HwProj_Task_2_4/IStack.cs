using System;

namespace StackCalc
{
	public interface IStack
	{
		void Push(int number);
		int Pop();
		bool IsEmpty { get; }
	}
}
