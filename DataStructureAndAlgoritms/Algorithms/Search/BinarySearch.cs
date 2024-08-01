using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Search
{
    public class BinarySearch
    {

        public static int Search(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length - 1;


            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (arr[mid] == target)
                {
                    return mid;
                }
                else if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            Console.WriteLine("No se encontro el valor");
            return -1; 
        }

        public static int Search(object[] arr, int target, string evaluatedProperty)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left <= right)
            {
                //comienza por el medio, si es menor baja si es mayor sube
                int mid = left + (right - left) / 2;

                if ((dynamic?)arr[mid].GetType().GetProperty(evaluatedProperty)?.GetValue(arr[mid])
                    == target)
                {
                    Console.WriteLine($"el indice de: {target} es {mid}");
                    return mid;
                }
                else if ((dynamic?)arr[mid].GetType().GetProperty(evaluatedProperty)?.GetValue(arr[mid])
                    < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            Console.WriteLine("elemento no encontrado");
            return -1;
        }
    }
}
