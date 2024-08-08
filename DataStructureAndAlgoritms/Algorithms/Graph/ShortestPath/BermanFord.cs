using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.ShortestPath
{
    public class BellmanFord
    {
        public static Dictionary<string, (int distance, string path)> ShortestPath(IGraph graph, string start)
        {
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var priorityQueue = new PriorityQueue<string, int>();

            //se toman todas las distancias como los valores mas altos
            foreach (var vertex in graph.GetVertices())
            {
                distances[vertex] = int.MaxValue;
                previous[vertex] = null;
            }

            distances[start] = 0;
            priorityQueue.Enqueue(start, 0);

            while (priorityQueue.Count > 0)
            {
                //el vertice desencolado forma parte de la distancia, asi obteniendo su valor
                //desencolara el valor mas bajo
                var vertex = priorityQueue.Dequeue();
                var distance = distances[vertex];
                //se buscas los vecinos
                foreach (var adjacent in graph.GetNeighbors(vertex))
                {
                    //alt es el valor normal del vecino, el distance[adjacent.item1] es el int.max value
                    var alt = distance + adjacent.Item2;
                    if (alt < distances[adjacent.Item1])
                    {
                        //el item1 para a alt para cuando empieze un nuevo recorrido
                        distances[adjacent.Item1] = alt;
                        //se graban la relacion en previous
                        previous[adjacent.Item1] = vertex;
                        //se agrega a la lista de prioridad, donde el menor valor sera el mas alto
                        priorityQueue.Enqueue(adjacent.Item1, alt);
                    }
                }
            }

            //una ves terminado y guardado todos los prvious se verifica si hay ciclos de peso negativo
            bool hasNegativeCycle = false;
            foreach (var vertex in graph.GetVertices())
            {
                foreach (var adjacent in graph.GetNeighbors(vertex))
                {
                    var alt = distances[vertex] + adjacent.Item2;
                    if (alt < distances[adjacent.Item1])
                    {
                        hasNegativeCycle = true;
                        break;
                    }
                }
                if (hasNegativeCycle) break;
            }

            if (hasNegativeCycle)
            {
                throw new InvalidOperationException("El grafo contiene un ciclo de peso negativo");
            }

            //reconstruir la ruta mas corta, se usa una tupla para la distancia y la ruta
            var shortestPath = new Dictionary<string, (int distance, string path)>();
            //se obtiene los vertices
            foreach (var vertex in graph.GetVertices())
            {
                if (vertex != start)
                {
                    var path = new List<string>();
                    var current = vertex;
                    while (current != null)
                    {
                        //se va agregando las referencias a path, por eso el previus, hasta que sea a null o no encuentre
                        path.Add(current);
                        current = previous[current];
                    }
                    path.Reverse();
                    shortestPath[vertex] = (distances[vertex], string.Join(" -> ", path));
                }
            }

            return shortestPath;
        }
    }
}
