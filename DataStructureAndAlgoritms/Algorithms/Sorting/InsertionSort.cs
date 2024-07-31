using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Sorting
{
    public class InsertionSort
    {
        public static void Sort(string evaluatedProperty, object[] objArray)
        {
            for (int i = 1; i < objArray.Length; i++)
            {
                object key = objArray[i];
                dynamic? keyValue = key.GetType().GetProperty(evaluatedProperty)?.GetValue(key);
                
                int j = i - 1;
                dynamic? prevValue = objArray[j].GetType().GetProperty(evaluatedProperty)?.GetValue(objArray[j]);

                //mientras el anterior sea mayor al actual, mueve los valores hacia adelante
                while (j >= 0 &&  prevValue > keyValue)
                {
                    objArray[j + 1] = objArray[j];
                    j = j - 1;
                }
                //e inserta ese valor en el lugar de atras donde quedo
                objArray[j + 1] = key;
            }
        }
    }
}
