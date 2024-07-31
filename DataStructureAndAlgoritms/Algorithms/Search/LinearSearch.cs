using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Search
{
    //no hay necesidad de explicarlo, recorre y busca
    public static class LinearSearch
    {
        public static int Search(int[] array, int targetValue)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == targetValue)
                {
                    Console.WriteLine("se encontro el valor" + i);
                    return i; 
                }
            }
            Console.WriteLine("No se encontro nada");
            return -1; // retorna -1 si no se encuentra
        }
        //IEquatable verifica si todas las propiedades son iguales, si todas lo son entonces lo considera
        //como el mismo objeto
        public static T? Search<T>(T[] array, T targetValue) where T : IEquatable<T>
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(targetValue))
                {
                    Console.WriteLine("se encontro el valor" + array[i]);
                    return array[i]; 
                }
            }
            Console.WriteLine("No se encontro nada");
            return default(T);
        }
        //funciona con comparadores preferiblemente para los objetos
        //le mandas una funcion tipada que debes conocer (desventaja) y devuelve bool, retorna el index
        public static int Search<T>(T[] array, T target, Func<T, T, bool> comparer)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (comparer(array[i], target))
                {
                    Console.WriteLine("Valor encontrado en " + i);
                    return i;
                }
            }
            Console.WriteLine("No se encontro el valor");
            return -1;
        }
    }
}
