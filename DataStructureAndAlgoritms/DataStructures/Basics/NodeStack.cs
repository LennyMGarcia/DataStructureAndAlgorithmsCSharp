using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Basics
{
    public class NodeStack<T>
    {
        private Node<T> _top;
        private int _size;

        public NodeStack()
        {
            _top = null;
            _size = 0;
        }

        public void Push(T data)
        {
            Node<T> newNode = new Node<T>(data);
            newNode.Next = _top;
            _top = newNode;
            _size++;
        }

        public T Pop()
        {
            if (_top == null)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            T data = _top.Data;
            _top = _top.Next;
            _size--;
            return data;
        }

        public T Peek()
        {
            if (_top == null)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return _top.Data;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public int Size()
        {
            return _size;
        }

        public void Clear()
        {
            _top = null;
            _size = 0;
        }

        private class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }
    }

    
}
