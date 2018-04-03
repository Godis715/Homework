using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QueueTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void CheckingAsSimpleQueue()
		{
			QueueHW_Proj.Queue<int> queue = new QueueHW_Proj.Queue<int>();
			queue.Enqueue(1, 1);
			queue.Enqueue(2, 1);
			queue.Enqueue(3, 1);
			Assert.AreEqual(1, queue.Dequeue());
			Assert.AreEqual(2, queue.Dequeue());
			Assert.AreEqual(3, queue.Dequeue());
		}

		[TestMethod]
		public void CheckingAsQueueWithWeights()
		{
			QueueHW_Proj.Queue<int> queue = new QueueHW_Proj.Queue<int>();
			queue.Enqueue(1, 1);
			queue.Enqueue(2, 2);
			queue.Enqueue(3, 3);
			Assert.AreEqual(3, queue.Dequeue());
			Assert.AreEqual(2, queue.Dequeue());
			Assert.AreEqual(1, queue.Dequeue());
		}

		[TestMethod]
		public void CheckingAsQueueWithWeightsAndSimpleQueue()
		{
			QueueHW_Proj.Queue<int> queue = new QueueHW_Proj.Queue<int>();
			queue.Enqueue(2, 1);
			queue.Enqueue(3, 1);
			queue.Enqueue(1, 2);
			queue.Enqueue(0, 3);
			queue.Enqueue(4, 1);
			Assert.AreEqual(0, queue.Dequeue());
			Assert.AreEqual(1, queue.Dequeue());
			Assert.AreEqual(2, queue.Dequeue());
			Assert.AreEqual(3, queue.Dequeue());
			Assert.AreEqual(4, queue.Dequeue());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void DequeueEmptyQueue()
		{
			QueueHW_Proj.Queue<int> queue = new QueueHW_Proj.Queue<int>();
			queue.Dequeue();
		}
	}
}
