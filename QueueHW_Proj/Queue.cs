using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueHW_Proj
{
	public class Queue<T>
	{
		private class Element
		{
			public Element(T value, int weight, Element next)
			{
				Value = value;
				Weight = weight;
				Next = next;
			}
			public T Value { get; }
			public Element Next { get; set; }
			public int Weight { get; }
		}
		private Element head;
		public Queue()
		{

		}
		public void Enqueue(T val, int weight)
		{
			if (head == null || head.Weight < weight)
			{
				head = new Element(val, weight, head);
				return;
			}

			Element current = head;

			while (current.Next != null && current.Next.Weight >= weight)
			{
				current = current.Next;
			}

			current.Next = new Element(val, weight, current.Next?.Next);

		}
		public T Dequeue()
		{
			if (head == null)
			{
				throw new InvalidOperationException("Queue.Dequeue: Queue was empty");
			}
			T value = head.Value;
			head = head.Next;
			return value;
		}
	}
}
