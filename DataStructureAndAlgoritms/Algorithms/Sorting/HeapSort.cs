using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//muchos comentarios xd
namespace DataStructureAndAlgorithms.Algorithms.Sorting
{

    public class HeapSort
    {
        public static void Sort(string evaluatedProperty, object[] array)
        {
            //Creamos un heap a partir del arreglo
            BuildHeap(evaluatedProperty, array);

            //extraemos los elementos del heap uno a uno y los colocamos en su posicion correcta
            for (int i = array.Length - 1; i > 0; i--)
            {
                //intercambiamos el elemento mas grande, raiz, con el ultimo elemento del arreglo
                //se intercambian con fines de verificacion, si resulto ser que el ultimo elemento era pequeno
                //entonces tuvo razon y permite usar el heapify en base a eso
                Swap(array, 0, i);

                // Reducimos el tamano del heap en 1
                //hace el trabajo pesado pero este es mas de verificacion, este es recursivo de por si
                Heapify(evaluatedProperty, array, 0, i - 1);
            }
        }
        //hace que se cumpla la propiedad max-heap, donde cada nodo es mayor o igual que sus hijos.
        //ejemplo al final
        private static void BuildHeap(string evaluatedProperty, object[] array)
        {
            //se considera que el arbol tiene siempre dos hijos y se reparte en partes iguales
            //el algoritmo lo hace porque trabaja con sub arboles pero eso el array.Lenght/2 - 1
            //siempre da el ultimo nodo que podria tener un hijo, se reduce en cada vuelta
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                Heapify(evaluatedProperty, array, i, array.Length - 1);
            }
        }
        //compara el nodo actual con sus hijos y si encuentra un hijo mayor, intercambia el nodo actual con ese hijo.
        private static void Heapify(string evaluatedProperty, object[] array, int index, int heapSize)
        {
            // indice padre
            int largest = index;

            // verificamos si el hijo izquierdo es mayor que el padre
            //la formula es para tener siempre un indice izquierdo o impar
            int leftChild = 2 * index + 1;
            if (leftChild <= heapSize 
                && (dynamic?)array[leftChild].GetType().GetProperty(evaluatedProperty)?.GetValue(array[leftChild]) 
                > (dynamic?)array[largest].GetType().GetProperty(evaluatedProperty)?.GetValue(array[largest]))
            {
                largest = leftChild;
            }

            //verificamos si el hijo derecho es mayor que el padre
            //aqui se tendria un idice par que es el derecho
            //son pseudohijos por asi decirlo
            int rightChild = 2 * index + 2;
            if (rightChild <= heapSize
                && (dynamic?)array[rightChild].GetType().GetProperty(evaluatedProperty)?.GetValue(array[rightChild])
                > (dynamic?)array[largest].GetType().GetProperty(evaluatedProperty)?.GetValue(array[largest]))
            {
                largest = rightChild;
            }

            //si el padre no es el mayor, intercambiamos con el hijo mas grande
            if (largest != index)
            {
                Swap(array, index, largest);
                Heapify(evaluatedProperty, array, largest, heapSize);
            }
        }

        private static void Swap(object[] array, int i, int j)
        {
            //intercambio
            object temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

//sin max-heap

//       4
//     /   \
//    10    3
//   / \   / \
//  5   1 7   6

//con mas heap despues del build heap

//       10
//     /    \
//    5      7
//   / \    / \
//  4   1  3   6