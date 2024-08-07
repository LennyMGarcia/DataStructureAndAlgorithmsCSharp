using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.MinimumSpanningTree
{
    public class Prim
    {
        public static Dictionary<string, int> MinimumSpanningTree(UndirectedWeightedGraph graph, string start)
        {
            var mst = new Dictionary<string, int>();
            var visited = new HashSet<string>();
            var edges = new PriorityQueue<(string, string, int), (string, string, int)>(new EdgeComparer());

            // Agregamos el vértice de inicio al MST con peso 0
            mst.Add(start, 0);
            visited.Add(start);

            foreach (var neighbor in graph.GetNeighbors(start))
            {
                edges.Enqueue((start, neighbor.Item1, neighbor.Item2), (start, neighbor.Item1, neighbor.Item2));
            }

            while (edges.Count > 0)
            {
                var minEdge = edges.Dequeue();
                var u = minEdge.Item1;
                var v = minEdge.Item2;
                var weight = minEdge.Item3;

                if (!visited.Contains(v))
                {
                    mst.Add(v, weight);
                    visited.Add(v);

                    foreach (var neighbor in graph.GetNeighbors(v))
                    {
                        if (!visited.Contains(neighbor.Item1))
                        {
                            edges.Enqueue((v, neighbor.Item1, neighbor.Item2), (v, neighbor.Item1, neighbor.Item2));
                        }
                    }
                }
            }

            return mst;
        }
    }

    public class EdgeComparer : IComparer<(string, string, int)>
    {
        public int Compare((string, string, int) x, (string, string, int) y)
        {
            return x.Item3.CompareTo(y.Item3);
        }
    }
}
