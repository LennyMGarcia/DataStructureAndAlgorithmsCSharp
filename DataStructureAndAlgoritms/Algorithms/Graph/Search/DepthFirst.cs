using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.Search
{
    public class DepthFirstSearch
    {
        public List<string> Search(IGraph graph, string startVertex)
        {
            var visited = new HashSet<string>();
            var result = new List<string>();

            DfsHelper(graph, startVertex, visited, result);

            return result;
        }

        private void DfsHelper(IGraph graph, string vertex, HashSet<string> visited, List<string> result)
        {
            visited.Add(vertex);
            result.Add(vertex);

            foreach (var neighbor in graph.GetNeighbors(vertex))
            {
                var neighborVertex = neighbor.Item1;
                if (!visited.Contains(neighborVertex))
                {
                    DfsHelper(graph, neighborVertex, visited, result);
                }
            }
        }
    }
}
