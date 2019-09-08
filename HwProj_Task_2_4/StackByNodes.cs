using System;

namespace StackCalc
{
	public class StackByNodes : IStack
	{
		private class Node
		{
			public Node Prev { get; set; }
			public int Value { get; set; }

			public Node(int value)
			{
				Prev = null;
				Value = value;
			}
		}

		private Node head;
		private bool isEmpty;
		public bool IsEmpty
		{
			get { return isEmpty; }
		}

		public StackByNodes()
		{
			head = null;
			isEmpty = true;
		}

		public void Push(int value)
		{
			Node newNode = new Node(value);
			newNode.Prev = head;
			head = newNode;
			isEmpty = false;
		}

		public int Pop()
		{
			if (isEmpty)
			{
				throw new IndexOutOfRangeException();
			}
			Node temp = head;
			head = head.Prev;
			if (head == null)
			{
				isEmpty = true;
			}
			return temp.Value;
		}

	}
}
