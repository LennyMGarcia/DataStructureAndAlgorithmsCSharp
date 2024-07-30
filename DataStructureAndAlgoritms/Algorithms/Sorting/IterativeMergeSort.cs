using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Sorting
{
    public class IterativeMergeSort
    {
        public static void Sort(int[] arr)
        {
            int length = arr.Length;
            int[] temp = new int[length];
            //primer for para incrementar geometricamente
            for (int size = 1; size < length; size *= 2)
            {
                //segundo for para elegir los valores izquierdos y los limites izquierdos y derechos
                for (int leftStart = 0; leftStart < length; leftStart += 2 * size)
                {
                    //aqui es donde principalmente se limita la lista a una serie de valores, comenzando desde el 0
                    //el limite principal izquierda es el primer elemento hasta el size - 1, que no sobrepase el tamano de la listaa
                    int mid = Math.Min(leftStart + size - 1, length - 1);
                    //limite derecho, es el doble del tamano de la lista menos un elemento izquierdo
                    int rightEnd = Math.Min(leftStart + 2 * size - 1, length - 1);

                    Merge(arr, temp, leftStart, mid, rightEnd);
                }
            }
        }

        private static void Merge(int[] arr, int[] temp, int left, int mid, int right)
        {
            int i = left; //valores de la lista real
            int j = mid + 1; //limite principal izquierda + 1
            int k = left; //valores de la lista temporal

            //bucle principal de comparacion
            while (i <= mid && j <= right)
            {
                //primer numero menor al segundo
                if (arr[i] <= arr[j])
                {
                    temp[k++] = arr[i++];
                }
                //entonces mayor
                else
                {
                    temp[k++] = arr[j++];
                }
            }
            //Una ves terminado el bucle principal asigna los elementos finales de la izquierda
            while (i <= mid)
            {
                temp[k++] = arr[i++];
            }
            //Asigna los elementos finales de la derecha, del limite del mid hasta el final del array
            while (j <= right)
            {
                temp[k++] = arr[j++];
            }
            //desde los valores de la izquierda hasta la derecha y los asigna
            for (int m = left; m <= right; m++)
            {
                arr[m] = temp[m];
            }
        }
    }
}
