using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Sorting
{
    public class RecursiveMergeSort
    {
        public static void Sort(string evaluatedProperty, object[] objArray)
        {
            MergeSort(objArray, 0, objArray.Length - 1, evaluatedProperty);
        }

        private static void MergeSort(object[] arr, int left, int right, string evaluatedProperty)
        {
            if (left < right)
            {
                int mid = (left + right) / 2; // divide el limite hasta que right sea menor a left
                // No se llama referencias recursivas dentro referencias recursivas por el desapilado, en cambio
                // se llama el sort debido al scope de cada referencia y funcionan como closure
                MergeSort(arr, left, mid, evaluatedProperty);//limite izquierdo
                MergeSort(arr, mid + 1, right, evaluatedProperty);//limite derecho

                Merge(arr, left, mid, right, evaluatedProperty);
            }
        }

        private static void Merge(object[] arr, int left, int mid, int right, string evaluatedProperty)
        {
            int i = left;
            int j = mid + 1;
            int k = 0;//comienza desde el indice 0 de la lista 
            object[] temp = new object[right - left + 1]; //la lista siempre mantiene los elementos actuales y el left mid ayudan a referencial los valores de la lista real

            while (i <= mid && j <= right)
            {
                if (
                    (dynamic?)arr[i].GetType().GetProperty(evaluatedProperty)?.GetValue(arr[i])
                    <=
                    (dynamic?)arr[j].GetType().GetProperty(evaluatedProperty)?.GetValue(arr[j])
                   )
                {
                    temp[k++] = arr[i++];
                }
                else
                {
                    temp[k++] = arr[j++];
                }
            }

            while (i <= mid)
            {
                temp[k++] = arr[i++];
            }

            while (j <= right)
            {
                temp[k++] = arr[j++];
            }

            for (int m = 0; m < temp.Length; m++)
            {
                arr[left + m] = temp[m];
            }
        }
    }
}