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
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var queue = new PriorityQueue<string, int>();

            distances[start] = 0;
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                var distance = distances[vertex];

                if (vertex == end)
                {
                    break;
                }

                foreach (var adjacent in graph.GetNeighbors(vertex))
                {
                    var alt = distance + adjacent.Item2;
                    if (!distances.ContainsKey(adjacent.Item1) || alt < distances[adjacent.Item1])
                    {
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
            while (current != start)
            {
                path.Add(current);
                current = previous[current];
            }
            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}
