using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructureAndAlgorithms.DataStructures.Graph
{

    public class DirectedWeightedGraph : IGraph
    {
        public Dictionary<string, HashSet<(string, int)>> _adjacent;

        public DirectedWeightedGraph()
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

        public void AddEdge(string origin, string destination, int weight)
        {
            if (_adjacent.ContainsKey(origin) && _adjacent.ContainsKey(destination))
            {
                _adjacent[origin].Add((destination, weight));
            }
            else
            {
                throw new Exception("El origen o el destino no existe");
            }
        }

        public void DeleteEdge(string origin, string destination)
        {
            if (_adjacent.ContainsKey(origin))
            {
                _adjacent[origin].RemoveWhere(adjacent => adjacent.Item1 == destination);
            }
            else
            {
                throw new Exception("El origen no existe");
            }
        }

        public void EditWeight(string origin, string destination, int newWeight)
        {
            if (_adjacent.ContainsKey(origin))
            {
                var edge = _adjacent[origin].FirstOrDefault(adjacent => adjacent.Item1 == destination);
                if (edge != default)
                {
                    _adjacent[origin].Remove(edge);
                    _adjacent[origin].Add((destination, newWeight));
                }
                else
                {
                    throw new Exception("Arista no existe");
                }
            }
            else
            {
                throw new Exception("El origen no existe");
            }
        }

        public int GetWeight(string origin, string destination)
        {
            if (_adjacent.ContainsKey(origin))
            {
                foreach (var adjacent in _adjacent[origin])
                {
                    if (adjacent.Item1 == destination)
                    {
                        return adjacent.Item2;
                    }
                }
            }
            throw new Exception("Arista no existe");
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
