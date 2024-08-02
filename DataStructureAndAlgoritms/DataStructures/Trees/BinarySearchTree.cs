using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Trees
{

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T>? _root;

        public BinarySearchTree()
        {
            _root = null;
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

        public void Insert(T value)
        {
            _root = InsertRecursive(_root, value);
        }
        //va bajando hacia aquellos que son mayores o aquellos menores, si encuentra null y es menor que el valor del nodo
        //lo inserta ahi, compareTo se encarga de eso
        private Node<T> InsertRecursive(Node<T>? node, T value)
        {
            if (node == null)
            {
                return new Node<T>(value);
            }

            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }

        public bool Search(T value)
        {
            return SearchRecursive(_root, value);
        }

        private bool SearchRecursive(Node<T>? node, T value)
        {
            if (node == null)
            {
                return false;
            }

            if (value.CompareTo(node.Value) == 0)
            {
                return true;
            }
            //busca hacia la derecha o la izuquierda dependiendo del valor
            //si es igual a  0 lo encontro
            if (value.CompareTo(node.Value) < 0)
            {
                return SearchRecursive(node.Left, value);
            }
            else
            {
                return SearchRecursive(node.Right, value);
            }
        }

        public void Delete(T value)
        {
            _root = DeleteRecursive(_root, value);
        }
        //
        private Node<T>? DeleteRecursive(Node<T>? node, T value)
        {
            if (node == null)
            {
                return null;
            }

            if (value.CompareTo(node.Value) < 0)
            {
                node.Left = DeleteRecursive(node.Left, value);
            }
            else if (value.CompareTo(node.Value) > 0)
            {
                node.Right = DeleteRecursive(node.Right, value);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    // si hay dos valores busca el minimo, para el izquierdo se basara en minNode
                    //aqui es como se encuentra el sucesor
                    Node<T> minNode = FindMin(node.Right);
                    node.Value = minNode.Value;
                    node.Right = DeleteRecursive(node.Right, minNode.Value);
                }
            }

            return node;
        }
        //Busca el minimo pues buscando hacia la izquierda hasta que sea diferente de null 
        private Node<T> FindMin(Node<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }
    }
}
