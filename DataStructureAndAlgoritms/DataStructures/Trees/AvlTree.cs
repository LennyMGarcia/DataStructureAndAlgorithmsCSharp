using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Trees
{
    public class AvlTreeNode
    {
        public int Value { get; set; }
        public AvlTreeNode Left { get; set; }
        public AvlTreeNode Right { get; set; }
        public int Height { get; set; }

        public AvlTreeNode(int value)
        {
            Value = value;
            Height = 1;
        }
    }

    public class AvlTree
    {
        private AvlTreeNode? _root;

        public void Print()
        {
            Print(_root, "", true);
        }

        private void Print(AvlTreeNode? node, string indent, bool isLast)
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

        public void Insert(int value)
        {
            _root = Insert(_root, value);
        }

        private AvlTreeNode Insert(AvlTreeNode? node, int value)
        {
            if (node == null)
            {
                return new AvlTreeNode(value);
            }

            if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value);
            }
            //no se permiten duplicados
            else
            {
                throw new InvalidOperationException("Duplicate value");
            }
            //Mientras mas avanzan mas aumenta el height del nodo, esto es para ayudar a
            //autobalancearse
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            //dependiendo del valor del autobalance hace que elija uno de los dos caminos
            int balanceFactor = GetBalanceFactor(node);
            // > 1 el izquierdo es mas grande que el derecho
            if (balanceFactor > 1)
            {
                //se balancea hacia la derecha y se asigna el valor
                if (value < node.Left.Value)
                {
                    //rota a la derecha, la explicacion esta abajo pero para uno solo
                    //en teoria es lo mismo que el rotateLeft pero hacia el otro lado
                    // si el rotate right coje el elemento derecho del lado izquiero
                    // y  usa el elemento derecho para pasarlo como root
                    // y el root pasa como referencia al lado derecho, teniendo el
                    // elemento derecho del lado izquiero anteriormente obtenido como valor del que antes era root
                    // asi se mantiene el balanceo en la rotacion
                    return RotateRight(node);
                }
                else
                {
                    //aqui balancea el lado izquierdo y luego con ese valor se balancea
                    //y devuelve el lado derecho

                    //si te lo imaginas realmente se recojen los nodos y se balancean
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }
            //< -1 el derecho es mas grande que el izquierdo
            if (balanceFactor < -1)
            {
                if (value > node.Right.Value)
                {
                    return RotateLeft(node);
                }
                else
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        public void Delete(int value)
        {
            _root = Delete(_root, value);
        }
        //similar al delete del BinarySearchTree pero con balanceo
        private AvlTreeNode Delete(AvlTreeNode? node, int value)
        {
            if (node == null)
            {
                return null;
            }

            if (value < node.Value)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete(node.Right, value);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    AvlTreeNode temp = FindMin(node.Right);
                    node.Value = temp.Value;
                    node.Right = Delete(node.Right, temp.Value);
                }
            }

            if (node == null)
            {
                return null;
            }

            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

            int balanceFactor = GetBalanceFactor(node);

            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(node.Left) >= 0)
                {
                    return RotateRight(node);
                }
                else
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            if (balanceFactor < -1)
            {
                if (GetBalanceFactor(node.Right) <= 0)
                {
                    return RotateLeft(node);
                }
                else
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private AvlTreeNode FindMin(AvlTreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        private AvlTreeNode RotateLeft(AvlTreeNode node)
        {
            //se coje el nodo derecho
            AvlTreeNode temp = node.Right;
            //el valor del nodo derecho ahora pasa a ser el valor del nodo izquierdo del nodo derecho
            node.Right = temp.Left;
            //el nodo derecho tiene el nodo pasado y su nodo derecho tiene la referencia del valor del nodo izquiero del nodo derecho
            // el nodo derecho paso a ser el nodo pasado, y el valor izquierdo del nodo derecho paso a ser el valor derecho del nodo pasado
            temp.Left = node;

            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            temp.Height = Math.Max(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

            return temp;
        }

        private AvlTreeNode RotateRight(AvlTreeNode node)
        {
            AvlTreeNode temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            temp.Height = Math.Max(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

            return temp;
        }

        private int GetHeight(AvlTreeNode node)
        {
            return node == null ? 0 : node.Height;
        }

        private int GetBalanceFactor(AvlTreeNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        public void InOrderTraversal()
        {
            InOrderTraversalRecursive(_root);
        }

        private void InOrderTraversalRecursive(AvlTreeNode? node)
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

        private void PreOrderTraversalRecursive(AvlTreeNode? node)
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

        private void PostOrderTraversalRecursive(AvlTreeNode? node)
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
