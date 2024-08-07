using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.Others
{
    public class FordFulkerson
    {
        private Dictionary<string, Dictionary<string, int>> _residualGraph;
        private DirectedWeightedGraph _graph;

        public FordFulkerson(DirectedWeightedGraph graph)
        {
            _graph = graph;
            _residualGraph = CreateResidualGraph();
        }

        private Dictionary<string, Dictionary<string, int>> CreateResidualGraph()
        {
            var residualGraph = new Dictionary<string, Dictionary<string, int>>();

            foreach (var vertex in _graph._adjacent.Keys)
            {
                residualGraph[vertex] = new Dictionary<string, int>();

                foreach (var adjacent in _graph._adjacent[vertex])
                {
                    residualGraph[vertex][adjacent.Item1] = adjacent.Item2;

                    if (!residualGraph.ContainsKey(adjacent.Item1))
                    {
                        residualGraph[adjacent.Item1] = new Dictionary<string, int>();
                    }

                    residualGraph[adjacent.Item1][vertex] = 0;
                }
            }

            foreach (var vertex in _graph._adjacent.Keys)
            {
                if (!residualGraph.ContainsKey(vertex))
                {
                    residualGraph[vertex] = new Dictionary<string, int>();
                }
            }

            return residualGraph;
        }

        public int MaxFlow(string source, string sink)
        {
            int maxFlow = 0;

            while (true)
            {
                var path = FindAugmentingPath(source, sink);
                if (path == null) break;

                int flow = CalculateFlow(path);
               

                maxFlow += flow;

                UpdateResidualGraph(path, flow);
            }

            Console.WriteLine($"Flujo maximo: {maxFlow}");

            return maxFlow;
        }

        private List<(string, string)> FindAugmentingPath(string source, string sink)
        {
            var queue = new Stack<string>();
            var parent = new Dictionary<string, string>();
            queue.Push(source);
            parent[source] = null;
            while (queue.Count > 0)
            {
                var vertex = queue.Pop();
                if (vertex == sink) break;
                foreach (var adjacent in _residualGraph[vertex])
                {
                    if (adjacent.Value > 0 && !parent.ContainsKey(adjacent.Key))
                    {
                        queue.Push(adjacent.Key);
                        parent[adjacent.Key] = vertex;
                       
                    }
                }

                foreach (var v in _graph._adjacent[vertex])
                {
                    if (!_residualGraph[vertex].ContainsKey(v.Item1) && v.Item2 > 0 && !parent.ContainsKey(v.Item1))
                    {
                        queue.Push(v.Item1);
                        parent[v.Item1] = vertex;
                    }
                }
            }
            if (!parent.ContainsKey(sink)) return null;
            var path = new List<(string, string)>();
            var current = sink;
            while (current != source)
            {
                path.Add((parent[current], current));
                current = parent[current];
            }
            path.Reverse();
            return path;
        }

        private int CalculateFlow(List<(string, string)> path)
        {
            int flow = int.MaxValue;
            foreach (var edge in path)
            {
                if (_residualGraph.ContainsKey(edge.Item1) && _residualGraph[edge.Item1].ContainsKey(edge.Item2))
                {
                    flow = Math.Min(flow, _residualGraph[edge.Item1][edge.Item2]);
                }
                else
                {
                    throw new Exception($"arista ({edge.Item1}, {edge.Item2}) no se encontraron en el grafo residual");
                }
            }
            return flow;
        }

        private void UpdateResidualGraph(List<(string, string)> path, int flow)
        {
             foreach (var edge in path)
             {
                if (_residualGraph.ContainsKey(edge.Item1) && _residualGraph[edge.Item1].ContainsKey(edge.Item2))
                {
                    _residualGraph[edge.Item1][edge.Item2] -= flow;
                }
                else
                {
                    throw new Exception($"arista ({edge.Item1}, {edge.Item2}) no se encontraron en el grafo residual");
                }

                if (_residualGraph.ContainsKey(edge.Item2))
                {
                    if (_residualGraph[edge.Item2].ContainsKey(edge.Item1))
                    {
                        _residualGraph[edge.Item2][edge.Item1] += flow;
                    }
                    else
                    {
                        _residualGraph[edge.Item2][edge.Item1] = flow;
                    }
                }
                else
                {
                    _residualGraph[edge.Item2] = new Dictionary<string, int>();
                    _residualGraph[edge.Item2][edge.Item1] = flow;
                }
            }
        }
    }
}
