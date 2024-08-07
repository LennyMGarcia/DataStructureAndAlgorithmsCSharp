using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.Search
{
    public class BreadthFirstSearch
    {
        public List<string> Search(IGraph graph, string startVertex)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var result = new List<string>();

            queue.Enqueue(startVertex);
            visited.Add(startVertex);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                result.Add(vertex);

                foreach (var neighbor in graph.GetNeighbors(vertex))
                {
                    var neighborVertex = neighbor.Item1;
                    if (!visited.Contains(neighborVertex))
                    {
                        queue.Enqueue(neighborVertex);
                        visited.Add(neighborVertex);
                    }
                }
            }

            return result;
        }
    }
}
