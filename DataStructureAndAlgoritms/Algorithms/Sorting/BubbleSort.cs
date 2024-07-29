using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Sorting
{
    public class BubbleSort
    {
        public static void Sort(string evaluatedProperty, object[] objArray)
        {
            bool Swapped;
            for (int i = 0; i < objArray.Length - 1; i++)
            {
                Swapped = false;
                for (int j = 0; j < objArray.Length - 1; j++) 
                {
                    dynamic? actualValue = objArray[j].GetType().GetProperty(evaluatedProperty)?.GetValue(objArray[j]);
                    dynamic? nextValue = objArray[j + 1].GetType().GetProperty(evaluatedProperty)?.GetValue(objArray[j + 1]);

                    if(actualValue == null || nextValue == null)
                    {
                        throw new InvalidOperationException("There are null");
                    }

                    if (actualValue > nextValue)
                    {
                        object temp = objArray[j];
                        objArray[j] = objArray[j+1];
                        objArray[j+1] = temp;
                        Swapped = true;
                    }
                }

                if (!Swapped)
                {
                    break;
                }
            }
        }
    }
}
