using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructureAndAlgorithms.DataStructures.Graph;

namespace DataStructureAndAlgorithms.Algorithms.Graph.ShortestPath
{
    public class Dijkstra
    {
        public static List<string> ShortestPath(IGraph graph, string start, string end)
        {
            //almacenadores, en el priority los valores mas pequenos tienen prioridad mas alta
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var queue = new PriorityQueue<string, int>();

            distances[start] = 0;
            queue.Enqueue(start, 0);

            //se agrega cada que puede con el fin de alcance el vetice final
            //al final se toma los elementos que forma la ruta total mas corta y su valor mas bajo se agrega a previus.
            while (queue.Count > 0)
            {
                //realmente la parte principal esta aqui, algunos elementos no pasaron el foreach y se quitan
                //desencolara el valor mas pequeno porque se puso arriba
                var vertex = queue.Dequeue();
                var distance = distances[vertex];
                
                //se obtienen los valores del vertice actual que son los vertices siguientes y su peso
                //GetNeighbors es IEnumerable
                foreach (var adjacent in graph.GetNeighbors(vertex))
                {
                    //se suma los valores del peso anterior (desencolado) y actual
                    // usa los valores basicos en ves del de distances
                    var alt = distance + adjacent.Item2;
                    //si es diferente del vertice o si es menor al de la distancia (comienza en el principal) se puede decir que es la anterior
                    //las distancias se van sumando
                    //esto evita problemas con el de la subruta no optima, se analiza si paso y si alt es menor
                    // la cola de prioridad hace que el menor valor se ponga arriba, aunque end se encuentr aqui, verifica otras rutas
                    if (!distances.ContainsKey(adjacent.Item1) || alt < distances[adjacent.Item1])
                    {
                        //se agrega un nuevo valor a la distancia,
                        //Se guarda el vertice anterior
                        //se encola los valores siempre que sea menor y se le agrega el peso
                        distances[adjacent.Item1] = alt;
                        previous[adjacent.Item1] = vertex;
                        queue.Enqueue(adjacent.Item1, alt);
                    }
                }
            }

            if (!distances.ContainsKey(end))
            {
                throw new Exception("No se encontro camino del comienzo hasta el final");
            }

            var path = new List<string>();
            var current = end;
            //se agrega los elementos anterior para simbolizar la ruta
            //usara los valores mas pequenos, pese a que se almacenan vertices de valores mas grandes
            //estas nunca llegan porque estan al final d
            while (current != start)
            {
                path.Add(current);
                current = previous[current];
            }
            //se agrega el primero y se ordena al reves para que comience desde start
            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}
