using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Trees
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }

        public Node(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class BinaryTree<T>
    {
        public Node<T>? Root { get; set; } = null;

        public BinaryTree()
        {
            Root = null;
        }

        public BinaryTree(T value)
        {
            Root = new Node<T>(value);
        }

        public void PrintTree(Node<T>? node, string indent = "")
        {
            if (node == null) return;

            Console.Write(indent);
            if (indent.Length > 0)
            {
                Console.Write(indent.Last() == 'l' ? "l" : "r");
            }
            Console.WriteLine(node.Value);
            PrintTree(node.Left, indent + "l");
            PrintTree(node.Right, indent + "r");
        }
        // si es la cabeza se asigna como root, sino usa el metodo de agregar recursivamente
        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new Node<T>(value);
            }
            else
            {
                AddRecursive(Root, value);
            }
        }

        private void AddRecursive(Node<T>? node, T value)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            //Icomparable ayuda a comparar. si es mayor retorna 1, si es menor retorna -1, si es igual retorna 0
            // si el nodo left o right es null, entonces se asigna, se usa la recursion para verificar hacia abajo
            if (value is IComparable<T> comparableValue)
            {
                if (comparableValue.CompareTo(node.Value) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new Node<T>(value);
                    }
                    else
                    {
                        
                        AddRecursive(node.Left, value);
                    }
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new Node<T>(value);
                    }
                    else
                    {
                        AddRecursive(node.Right, value);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot compare values of type " + typeof(T).Name);
            }
        }
        //la recursividad funciona diferente con los objetos por su referencias
        //dependiendo del lugar del console.log llamara al numero, esto funciona con las asignaciones si quieres hacer un array con esos
        public void InOrderTraversal()
        {
            InOrderTraversalRecursive(Root);
        }

        private void InOrderTraversalRecursive(Node<T>? node)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left);
                Console.WriteLine(node.Value);
                InOrderTraversalRecursive(node.Right);
            }
        }

        public void PreOrderTraversal()
        {
            PreOrderTraversalRecursive(Root);
        }

        private void PreOrderTraversalRecursive(Node<T>? node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Value);
                PreOrderTraversalRecursive(node.Left);
                PreOrderTraversalRecursive(node.Right);
            }
        }

        public void PostOrderTraversal()
        {
            PostOrderTraversalRecursive(Root);
        }

        private void PostOrderTraversalRecursive(Node<T>? node)
        {
            if (node != null)
            {
                PostOrderTraversalRecursive(node.Left);
                PostOrderTraversalRecursive(node.Right);
                Console.WriteLine(node.Value);
            }
        }
    }
}
