using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructureAndAlgorithms.DataStructures.Graph
{
    public class UndirectedWeightedGraph : IGraph
    {
        public Dictionary<string, HashSet<(string, int)>> _adjacent;

        public UndirectedWeightedGraph()
        {
            _adjacent = new Dictionary<string, HashSet<(string, int)>>();
        }

        public void AddVertex(string vertex)
        {
            if (!_adjacent.ContainsKey(vertex))
            {
                _adjacent[vertex] = new HashSet<(string, int)>();
            }
        }

        public void AddEdge(string vertex1, string vertex2, int weight)
        {
            if (_adjacent.ContainsKey(vertex1) && _adjacent.ContainsKey(vertex2))
            {
                _adjacent[vertex1].Add((vertex2, weight));
                _adjacent[vertex2].Add((vertex1, weight)); 
            }
            else
            {
                throw new Exception("Uno de los dos vertices no existe");
            }
        }

        public int GetWeight(string vertex1, string vertex2)
        {
            if (_adjacent.ContainsKey(vertex1) && _adjacent.ContainsKey(vertex2))
            {
                foreach (var adjacent in _adjacent[vertex1])
                {
                    if (adjacent.Item1 == vertex2)
                    {
                        return adjacent.Item2;
                    }
                }
            }
            throw new Exception("Arista no existe o no se encontro");
        }

        public IEnumerable<string> GetVertices()
        {
            return _adjacent.Keys;
        }

        public IEnumerable<(string, int)> GetNeighbors(string vertex)
        {
            return _adjacent[vertex];
        }

        public List<(string, string, int)> GetEdges()
        {
            var edges = new List<(string, string, int)>();
            foreach (var vertex in _adjacent)
            {
                foreach (var adjacent in vertex.Value)
                {
                    edges.Add((vertex.Key, adjacent.Item1, adjacent.Item2));
                }
            }
            return edges;
        }
    }
}
