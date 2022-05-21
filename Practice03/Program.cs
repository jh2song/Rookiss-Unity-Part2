/* Priority Queue Implementation Practice
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice03
{
	class PriorityQueue<T> where T : IComparable<T>
	{
		List<T> _heap = new List<T>();
		public int Count { get { return _heap.Count; } }

		public void Push(T data)
		{
			_heap.Add(data);
			int child = Count - 1;
			int parent = (child - 1) / 2;

			while (child > 0)
			{
				if (_heap[child].CompareTo(_heap[parent]) < 0)
					break;

				T temp = _heap[child];
				_heap[child] = _heap[parent];
				_heap[parent] = temp;

				child = parent;
				parent = (child - 1) / 2;
			}
		}

		public T Pop()
		{
			T ret = _heap[0];
			_heap[0] = _heap[Count - 1];
			_heap.RemoveAt(Count - 1);

			int parent = 0;
			while (true)
			{
				int left = parent * 2 + 1;
				int right = parent * 2 + 2;

				int candidate = parent;
				if (left < Count && _heap[candidate].CompareTo(_heap[left]) < 0)
					candidate = left;
				if (right < Count && _heap[candidate].CompareTo(_heap[right]) < 0)
					candidate = right;

				if (candidate == parent)
					break;

				T temp = _heap[parent];
				_heap[parent] = _heap[candidate];
				_heap[candidate] = temp;

				parent = candidate;
			}

			return ret;
		}
	}
	
	struct Knight : IComparable<Knight>
	{
		public int Id { get; set; }

		public int CompareTo(Knight other)
		{
			if (this.Id > other.Id) return 1;
			else if (this.Id < other.Id) return -1;
			else return 0;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			PriorityQueue<int> pq = new PriorityQueue<int>();
			pq.Push(40);
			pq.Push(30);
			pq.Push(70);
			pq.Push(10);
			pq.Push(90);

			/*
			90
			70
			40
			30
			10
			*/
			while (pq.Count > 0)
			{
				Console.WriteLine(pq.Pop());
			}

			PriorityQueue<Knight> pq2 = new PriorityQueue<Knight>();
			pq2.Push(new Knight() { Id = 30 });
			pq2.Push(new Knight() { Id = 20 });
			pq2.Push(new Knight() { Id = 40 });
			pq2.Push(new Knight() { Id = 10 });
			pq2.Push(new Knight() { Id = 50 });

			while (pq2.Count > 0)
			{
				Console.WriteLine(pq2.Pop().Id);
			}

		}
	}
}
