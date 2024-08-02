
using System;
using System.Collections.Generic;

namespace DataStructureAndAlgorithms.DataStructures.Basics
{

    public class Node
    {
        public object Data { get; set; }
        public Node? Next { get; set; }

        public Node(object data)
        {
            Data = data;
            Next = null;
        }
    }

    public class SingleLinkedList
    {
        public Node? Head { get; private set; }

        public void AddFirst(object data)
        {
            Node newNode = new Node(data);
            newNode.Next = Head;
            Head = newNode;
        }

        public void AddLast(object data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                Node current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new Node(data);
            }
        }

        public void AddAt(int index, object data)
        {
            if (index == 0)
            {
                AddFirst(data);
            }
            else
            {
                Node? current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    if (current?.Next == null)
                    {
                        break;
                    }
                    current = current.Next;
                }
                Node newNode = new Node(data);
                newNode.Next = current?.Next;
                current.Next = newNode;
            }
        }

        public void DeleteFirst()
        {
            if (Head != null)
            {
                Head = Head.Next;
            }
        }

        public void DeleteLast()
        {
            if (Head == null)
            {
                return;
            }
            else if (Head.Next == null)
            {
                Head = null;
            }
            else
            {
                Node current = Head;
                while (current?.Next?.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null;
            }
        }

        public void DeleteAt(int index)
        {
            if (index == 0)
            {
                DeleteFirst();
            }
            else
            {
                Node? current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    if (current?.Next == null)
                    {
                        break;
                    }
                    current = current.Next;
                }
                if (current?.Next != null)
                {
                    current.Next = current.Next.Next;
                }
            }
        }

        public void PrintList()
        {
            Node? current = Head;
            while (current != null)
            {
                Console.Write("El dato actual es: " + current.Data + "\n");
                current = current?.Next;
            }
            Console.WriteLine(new String('-', 80));
        }
    }

}