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
        public static Dictionary<string, int> ShortestPath(IGraph graph, string start)
        {
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();

            foreach (var vertex in graph.GetVertices())
            {
                distances[vertex] = int.MaxValue;
                previous[vertex] = null;
            }

            distances[start] = 0;

            for (int i = 0; i < graph.GetVertices().Count() - 1; i++)
            {
                foreach (var vertex in graph.GetVertices())
                {
                    foreach (var adjacent in graph.GetNeighbors(vertex))
                    {
                        var alt = distances[vertex] + adjacent.Item2;
                        if (alt < distances[adjacent.Item1])
                        {
                            distances[adjacent.Item1] = alt;
                            previous[adjacent.Item1] = vertex;
                        }
                    }
                }
            }

            // Check for negative-weight cycles
            foreach (var vertex in graph.GetVertices())
            {
                foreach (var adjacent in graph.GetNeighbors(vertex))
                {
                    var alt = distances[vertex] + adjacent.Item2;
                    if (alt < distances[adjacent.Item1])
                    {
                        throw new Exception("El grafo contiene un ciclo de peso negativo");
                    }
                }
            }

            return distances;
        }
    }
}
