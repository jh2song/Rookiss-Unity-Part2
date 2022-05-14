using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._3.환경설정
{
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null; // 첫번째
        public MyLinkedListNode<T> Tail = null; // 마지막
        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>();
            newNode.Data = data;

            // 만약에 아직 방이 아예 없었다면 새로 추가된 첫번째 방이 곧 Head이다.
            if (Head == null)
                Head = newNode;

            // 기존의 [마지막 방]과 [새로 추가되는 방]을 연결해준다.
            if (Tail != null)
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
            }

            // [새로 추가되는 방]을 [마지막 방]으로 인정한다.
            Tail = newNode;
            Count++;
            return newNode;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> node)
        {
            // [기존의 첫번째 방의 다음 방]을 [첫번째 방으로] 인정한다.
            if (Head == node)
                Head = Head.Next;

            // [기존의 마지막 방의 이전 방]을 [마지막 방으로] 인정한다.
            if (Tail == node)
                Tail = Tail.Prev;

            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            Count--;
        }
    }

    class Board
    {
        public int[] _data = new int[25]; // 배열
        public MyLinkedList<int> _data3 = new MyLinkedList<int>(); // 연결 리스트

        public void Initialize()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);

            _data3.Remove(node);
        }
    }
}
