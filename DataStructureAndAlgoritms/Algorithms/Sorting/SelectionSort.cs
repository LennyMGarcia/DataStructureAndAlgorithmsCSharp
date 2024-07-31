using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Sorting
{
    public class SelectionSort
    {
        public static void Sort(string evaluatedProperty, object[] objArray) 
        {
            int length = objArray.Length;

            for (int i = 0; i < length - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < length; j++)
                {
                    dynamic? actualValue = objArray[j].GetType().GetProperty(evaluatedProperty)?.GetValue(objArray[j]);
                    dynamic? minIndexValue = objArray[minIndex].GetType().GetProperty(evaluatedProperty)?.GetValue(objArray[minIndex]);

                    if (actualValue  == null || minIndexValue == null)
                    {
                        throw new InvalidOperationException("There are null");
                    }
                    //va buscando que elemento va a seleccionar, si es ese elemento actual menor al al del indice minimo
                    //el indice minimo se mueve
                    if (actualValue < minIndexValue)
                    {
                        minIndex = j;
                    }
                }
                //si ese indice minimo es diferente de si mismo
                //intercambia el valor seleccionado con el del indice siempre que sea mas pequeno
                if(minIndex != i)
                {
                    object temp = objArray[i];
                    objArray[i] = objArray[minIndex];
                    objArray[minIndex] = temp;
                }
            }
        }
    }
}
