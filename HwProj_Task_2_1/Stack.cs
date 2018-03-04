using System;

namespace HwProj_Task_2_1
{
	public class Stack
	{
		private class Node
		{
			public Node Prev { get; set; }
			public int Value { get; set; }

			public Node(int value) {
				Prev = null;
				Value = value;
			}
		}

		private Node head;
		private int size;
		public int Size
		{
			get { return size; }
		}

		public Stack() {
			head = null;
			size = 0;
		}

		public void Push(int value)
		{
			Node newNode = new Node(value);
			newNode.Prev = head;
			head = newNode;
			++size;
		}

		public int Pop() {
			if (Size == 0) {
				throw new IndexOutOfRangeException();
			}
			Node temp = head;
			head = head.Prev;
			--size;
			return temp.Value;
		}

	}
}
