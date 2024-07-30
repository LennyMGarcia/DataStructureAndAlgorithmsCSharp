using System;

public class QuickSort
{
    public static void Sort(string evaluatedProperty,object[] array)
    {
        quickSort(evaluatedProperty,array, 0, array.Length - 1);
    }

    private static void quickSort(string evaluatedProperty, object[] array, int left, int right)
    {
        if (left < right)
        {
            //el valor del pivote detemina la derecha  y la izquierda
            //el incremento funciona como limites y dice hacia donde avanzar
            int pivot = Partition(evaluatedProperty, array, left, right);
            quickSort(evaluatedProperty, array, left, pivot - 1);
            quickSort(evaluatedProperty, array, pivot + 1, right);
        }
    }

    private static int Partition(string evaluatedProperty, object[] array, int left, int right)
    {
        dynamic? pivot = array[right].GetType().GetProperty(evaluatedProperty)?.GetValue(array[right]);
        int i = left - 1;

        //Intercambia aquellos elementos que son menores al ultimo
        //aunque no se cambia el pivote, los elementos se ordenan por referencia del pivote
        //si todos son mayores al ultimo, al final se hace intercambio pero no en el bucle
        //los elementos pequenos suelen irse hacia atras por i
        for (int j = left; j < right; j++)
        {
            if ((dynamic?)array[j].GetType().GetProperty(evaluatedProperty)?.GetValue(array[j]) 
                <= pivot)
            {
                i++;
                object temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        //En en caso de que i sea mayor al ultimo significa que el ultimo es el mas pequeno
        //este es el que se encarga de mover el pivote
        object temp2 = array[i + 1];
        array[i + 1] = array[right];
        array[right] = temp2;

        return i + 1;
    }
}