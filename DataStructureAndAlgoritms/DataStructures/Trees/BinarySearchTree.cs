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

        public void Print()
        {
            Print(_root, "", true);
        }

        private void Print(Node<T>? node, string indent, bool isLast)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (isLast)
                {
                    Console.Write("R----");
                    indent += "     ";
                }
                else
                {
                    Console.Write("L----");
                    indent += "|    ";
                }

                Console.WriteLine(node.Value);

                Print(node.Left, indent, false);
                Print(node.Right, indent, true);
            }
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

                    //aqui hay algo curioso, si eliminas uno de los primeros parece que el nodo cambio de lugar
                    //en realidad solo cambio su valor en la referencia y se elimino este de abajo
                    // la recursion va hacia arriba recordando el valor y asignando de abajo hacia arriba
                    //por eso los nuevos objetos tienen nuevas referencias a medida de que sube
                    //todos los objetos se hacen inserciones en tiempo real 
                    Node<T> minNode = FindMin(node.Right);
                    node.Value = minNode.Value;
                    node.Right = DeleteRecursive(node.Right, minNode.Value);
                }
            }
            //cada recursividad retorna esto
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

        public void InOrderTraversal()
        {
            InOrderTraversalRecursive(_root);
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
            PreOrderTraversalRecursive(_root);
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
            PostOrderTraversalRecursive(_root);
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
